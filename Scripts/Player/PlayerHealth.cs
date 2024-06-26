using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public BoxCollider2D PlayerCollider;

    public float HP { get; set; }
    public float MaxHP { get; set; }
    public float Attack { get; set; }
    public float AttackSpeed {  get; set; }
    public float CriticalRate {  get; set; }
    public float CriticalDamage {  get; set; }
    public float Defence { get; set; }

    public float Damage { get; set; }
    public float AttackInterval { get; set; }

    private Coroutine damageCoroutine;
    
    private float _eDefence;

    private float _eInterval;


    private void Awake()
    {
        PlayerCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        InitStat();
    }

    public void InitStat()
    {
        MaxHP = GameManager.Instance.MaxHP;
        Attack = GameManager.Instance.Attack;
        AttackSpeed = GameManager.Instance.AttackSpeed;
        CriticalRate = GameManager.Instance.CriticalRate;
        CriticalDamage = GameManager.Instance.CriticalDamage;
        Defence = GameManager.Instance.Defence;

        HP = MaxHP;

        UIManager.Instance.UpdatePlayerHPUI(MaxHP, HP);
    }

    private float GetDamage()
    {
        Damage = (Attack - _eDefence) * (Attack / (Attack + _eDefence));
        if(Damage <= 0)
        {
            Damage = 1;
        }
        return Damage;
    }

    public float SetInterval()
    {
        AttackInterval = 1.0f / AttackSpeed;
        return AttackInterval;
    }

    // 접촉한 몬스터에게 플레이어의 데미지만큼 피해를 입힘
    private void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("접촉!");
        if (other.TryGetComponent(out EnemyHealth health))
        {
            _eDefence = health.Defence;
            _eInterval = health.SetInterval();
            health.TakeDamage(GetDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);
    }


    //몬스터의 데미지에 비례해 플레이어 체력 감소
    public void TakeDamage(float damage)
    {
        damageCoroutine = StartCoroutine(TakeDamageRepeatedly(damage));
    }

    IEnumerator TakeDamageRepeatedly(float damage)
    {
        while (HP >= 0)
        {
            
            HP = Mathf.Max(HP - damage, 0);
            GameManager.Instance.HP = HP;
            UIManager.Instance.UpdatePlayerHPUI(MaxHP,HP);
            //Debug.Log(HP);
            
            if (HP <= 0)
            {
                //플레이어 사망 이벤트
                Debug.Log("플레이어 사망");
                if (damageCoroutine != null)
                    StopCoroutine(damageCoroutine);
                GameManager.Instance.StageSystem.SpawnedEnemy.GetComponent<EnemyHealth>().StopDamageCoroutine();
                GameManager.Instance.Player.stateMachine.ChangeState(GameManager.Instance.Player.stateMachine.DieState);
            }
            yield return new WaitForSeconds(_eInterval);
        }
        
        

    }

}
