using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [field: Header("Reference")]
    [field: SerializeField] public EnemyStat Data { get; private set; }
    [field: SerializeField] public BossStat BossData { get; private set; }

    public BoxCollider2D EnemyCollider;

    public float HP { get; set; }
    public float MaxHP { get; set; }
    public float Attack { get; set; }
    public float Defence { get; set; }
    public string EnemyName { get; set; }

    public float Damage { get; set; }
    public float AttackInterval {  get; set; }

    public Coroutine damageCoroutine;
    public event Action OnDie;

    private float _pDefence;
    private float _pCriticalRate;
    private float _pCriticalDamage;
    private float _pInterval;
    private int _stageNum;
    private void Awake()
    {
        EnemyCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        /*HP = Data.EnemyHP;
        Attack = Data.EnemyAttack;
        Defence = Data.EnemyDefence;*/
        SetEnemyStat();
    }
    public void SetEnemyStat()
    {
        _stageNum = StageSystem.Instance.StageData.StageNum;
        if(_stageNum %10 == 0)
        {
            //Boss
            SetBossstat(_stageNum);
            gameObject.transform.localScale = new Vector3(4, 4, 4);
            GameManager.Instance.StageSystem.StageData.IsBossStage = true;
        }
        else
        {
            //Normal Enemy
            EnemyName = Data.EnemyNames[_stageNum % 13];
            MaxHP = Data.EnemyHP + (1 + Data.HpRate * (_stageNum - 1));
            Attack = Data.EnemyAttack + (1 + Data.AttackRate * (_stageNum - 1));
            Defence = Data.EnemyDefence + (1 + Data.AttackRate * (_stageNum - 1));
            HP = MaxHP;
            GameManager.Instance.StageSystem.StageData.IsBossStage = false;
        }
        
        UIManager.Instance.UpdateEnemyHpUI(MaxHP, HP);
        UIManager.Instance.UpdateEnemyNameUI(EnemyName);

    }
    void SetBossstat(int stagenum)
    {
        if(stagenum != 10)
        {
            MaxHP = BossData.BossHP + (((_stageNum / 10) - 1) * BossData.HpRate);
            Attack = BossData.BossAttack + (((_stageNum / 10) - 1) * BossData.AttackRate);
            Defence = BossData.BossDefence + (((_stageNum / 10) - 1) * BossData.DefenceRate);           
        }
        else
        {
            MaxHP = BossData.BossHP;
            Attack = BossData.BossAttack;
            Defence = BossData.BossDefence;
        }
        EnemyName = BossData.BossNames[stagenum % 13];
        HP = MaxHP;
    }
    private float GetDamage()
    {
        Damage = Attack-(_pDefence/Attack);
        if(Damage <= 0)
        {
            Damage = 1;
        }
        return Damage;
    }

    public float SetInterval()
    {
        AttackInterval = 1.0f;
        return AttackInterval;
    }

    // 접촉한 플레이어에게 몬스터의 데미지만큼 피해를 입힘

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth health))
        {
            _pDefence = health.Defence;
            _pCriticalRate = health.CriticalRate;
            _pCriticalDamage = health.CriticalDamage;
            _pInterval = health.SetInterval();
            health.TakeDamage(GetDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(damageCoroutine);
    }

// 데미지에 비례해 몬스터 체력 감소

    public void TakeDamage(float damage)
    {
        damageCoroutine = StartCoroutine(TakeDamageRepeatedly(damage));
    }

    IEnumerator TakeDamageRepeatedly(float damage)
    {
        while (HP > 0)
        {
            
            float critical = UnityEngine.Random.value;
            if(critical <= _pCriticalRate)
            {                
                damage = damage * _pCriticalDamage;
                SoundManager.Instance.CriticalAttackSound();
                Debug.Log($"크리티컬");
            }
            else
            {
                SoundManager.Instance.PlayerAttackSound();
            }
            damage = Mathf.Round(damage * 10) / 10;
            HP = Mathf.Max(HP - damage, 0);
            Debug.Log($"damage {damage}");
            // 적 생성될때 MaxHp값을 만들어서 Data.EnemyHP 대신 입력
            UIManager.Instance.UpdateEnemyHpUI(MaxHP, HP);
            if (HP <= 0)
            {
                Debug.Log("몬스터 사망");
                GameManager.Instance.StageSystem.StageClearEvent.Invoke();
               //몬스터 사망 이벤트
                OnDie?.Invoke();
            }
            yield return new WaitForSeconds(_pInterval);          
            //Debug.Log(HP);
        }

    }

    public void StopDamageCoroutine()
    {
        StopCoroutine(damageCoroutine);
    }
}
