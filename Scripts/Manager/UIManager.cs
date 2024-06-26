using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //public PlayerStat PlayerStat;
    public Player player;

    // 스탯 레벨 당 증가량
    private float AtkIncrease = 10f;
    private float DefIncrease = 2f;
    private float HPIncrease = 10f;
    private float CriticalDmgIncrease = 0.01f;
    private float CriticalRateIncrease = 0.015f;
    private float AtkSpeedIncrease = 0.05f;

    // UI 요소
    public GameObject statList;
    public GameObject itemList;
    public GameObject partyList;
    public GameObject shopList;

    // 텍스트 요소
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI jewel;
    public TextMeshProUGUI floor;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI monsterHpTxt;
    public TextMeshProUGUI goldNum;
    
    public Image warningUI;  // 경고 UI

    // 이미지 요소
    public Image monsterHpBar;
    public Image monsterHp;

    public Image playerHpBar;
    public TextMeshProUGUI playerHptext;

    // 스탯 및 아이템 타입 리스트
    public List<Stat> statTypes;
    public List<Image> itemTypes;

    private void Start()
    {
        if (goldNum == null)
        {
            Debug.LogError("GoldNum 텍스트 컴포넌트가 설정되지 않았습니다!");
            return;
        }
        InitializeStats();
        UpdateGoldUI();
        warningUI.gameObject.SetActive(false);  // 경고 UI 초기 비활성화
    }

    // 골드 변수
   /* private int Gold
    {
        get
        {
            int goldValue;
            if (int.TryParse(goldNum.text, out goldValue))
            {
                return goldValue;
            }
            else
            {
                Debug.LogError("GoldNum 텍스트 컴포넌트의 값이 유효한 정수가 아닙니다!");
                return 0;
            }
        }
        set
        {
            goldNum.text = value.ToString();
            UpdateGoldUI();
        }
    }*/

    // 스탯 초기화
    private void InitializeStats()
    {
        foreach (var stat in statTypes)
        {
            switch (stat.statType)
            {
                case StatType.HP:
                    stat.baseValue = GameManager.Instance.MaxHP;
                    break;
                case StatType.Attack:
                    stat.baseValue = GameManager.Instance.Attack;
                    break;
                case StatType.Defense:
                    stat.baseValue = GameManager.Instance.Defence;
                    break;
                case StatType.Speed:
                    stat.baseValue = GameManager.Instance.AttackSpeed;
                    break;
                case StatType.CriticalRate:
                    stat.baseValue = GameManager.Instance.CriticalRate;
                    break;
                case StatType.CriticalDamage:
                    stat.baseValue = GameManager.Instance.CriticalDamage;
                    break; ;
                default:
                    break;
            }

            stat.statValue.text = GetStatDisplayValue(stat);
            stat.statLv.text = stat.level.ToString();
            stat.lvPrice.text = CalculateUpgradeCost(stat.level).ToString();

            // 레벨업 버튼 클릭 이벤트 추가
            stat.lvUpBtn.onClick.AddListener(() => LvUpStat(stat));
        }
    }
    public void UpdateStatData()
    {
        foreach (var stat in statTypes)
        {
            switch (stat.statType)
            {
                case StatType.HP:
                    stat.baseValue = GameManager.Instance.MaxHP;
                    break;
                case StatType.Attack:
                    stat.baseValue = GameManager.Instance.Attack;
                    break;
                case StatType.Defense:
                    stat.baseValue = GameManager.Instance.Defence;
                    break;
                case StatType.Speed:
                    stat.baseValue = GameManager.Instance.AttackSpeed;
                    break;
                case StatType.CriticalRate:
                    stat.baseValue = GameManager.Instance.CriticalRate;
                    break;
                case StatType.CriticalDamage:
                    stat.baseValue = GameManager.Instance.CriticalDamage;
                    break; ;
                default:
                    break;
            }
            stat.statValue.text = GetStatDisplayValue(stat);
            UIManager.Instance.UpdatePlayerHPUI(GameManager.Instance.MaxHP, GameManager.Instance.HP);
        }

    }

    // 스탯 표시값 생성
    private string GetStatDisplayValue(Stat stat)
    {
        if (stat.statType == StatType.CriticalRate || stat.statType == StatType.CriticalDamage)
        {
            return $"{stat.baseValue * 100}%";
        }
        else
        {
            return stat.baseValue.ToString();
        }
    }

    // 스탯 리스트 표시
    public void ShowStatList()
    {
        statList.SetActive(true);
        itemList.SetActive(false);
        partyList.SetActive(false);
        shopList.SetActive(false);
    }

    // 아이템 리스트 표시
    public void ShowItemList()
    {
        statList.SetActive(false);
        itemList.SetActive(true);
        partyList.SetActive(false);
        shopList.SetActive(false);
    }
    public void ShowPartyList()
    {
        statList.SetActive(false);
        itemList.SetActive(false);
        partyList.SetActive(true);
        shopList.SetActive(false);
    }
    public void ShowShopList()
    {
        statList.SetActive(false);
        itemList.SetActive(false);
        partyList.SetActive(false);
        shopList.SetActive(true);
    }

    // 스탯 레벨업 함수
    public void LvUpStat(Stat stat)
    {
        int price = CalculateUpgradeCost(stat.level); // 레벨업 가격 계산
        if (GameManager.Instance.currency.IsUpgradable(price))
        {
            GameManager.Instance.currency.gold -= price; // 골드 차감

            // 스탯 값 업데이트
            switch (stat.statType)
            {
                case StatType.HP:
                    stat.statValue.text = (stat.baseValue + HPIncrease).ToString();
                    GameManager.Instance.MaxHP = stat.baseValue + HPIncrease ;
                    GameManager.Instance.HP += HPIncrease;
                    UpdatePlayerHPUI(GameManager.Instance.MaxHP, player.health.HP);
                    break;
                case StatType.Attack:
                    stat.statValue.text = (stat.baseValue + AtkIncrease).ToString();
                    GameManager.Instance.Attack = stat.baseValue + AtkIncrease;
                    break;
                case StatType.Defense:
                    stat.statValue.text = (stat.baseValue + DefIncrease).ToString();
                    GameManager.Instance.Defence = stat.baseValue + DefIncrease;
                    break;
                case StatType.Speed:
                    stat.statValue.text = (stat.baseValue + AtkSpeedIncrease).ToString();
                    GameManager.Instance.AttackSpeed = stat.baseValue + AtkSpeedIncrease;
                    break;
                case StatType.CriticalRate:
                    stat.statValue.text = $"{(stat.baseValue + CriticalRateIncrease) * 100}%";
                    GameManager.Instance.CriticalRate = stat.baseValue + CriticalRateIncrease;
                    break;
                case StatType.CriticalDamage:
                    stat.statValue.text = $"{(stat.baseValue + CriticalDmgIncrease) * 100}%";
                    GameManager.Instance.CriticalDamage = stat.baseValue + CriticalRateIncrease;
                    break;
                default:
                    break;
            }
            stat.level++; // 스탯 레벨 증가
            stat.statLv.text = stat.level.ToString(); // 스탯 레벨 텍스트 업데이트
            stat.lvPrice.text = CalculateUpgradeCost(stat.level).ToString(); // 다음 레벨업 가격 업데이트
            UpdateGoldUI(); // 골드 UI 업데이트
            UpdateStatData();
            UpdatePlayerStat();
        }
        else
        {
            StartCoroutine(ShowWarningUI());
        }
    }
    public void UpdatePlayerStat()
    {
        player.health.HP = GameManager.Instance.HP;
        player.health.MaxHP = GameManager.Instance.MaxHP;
        player.health.Attack = GameManager.Instance.Attack;
        player.health.AttackSpeed = GameManager.Instance.AttackSpeed;
        player.health.CriticalRate = GameManager.Instance.CriticalRate;
        player.health.CriticalDamage = GameManager.Instance.CriticalDamage;
        player.health.Defence = GameManager.Instance.Defence;
    }

    // 경고 UI 표시 함수
    private IEnumerator ShowWarningUI()
    {
        warningUI.gameObject.SetActive(true);
        Color warningColor = warningUI.color;
        warningColor.a = 1f;
        warningUI.color = warningColor;

        yield return new WaitForSeconds(2);

        while (warningUI.color.a > 0)
        {
            warningColor.a -= Time.deltaTime / 2;
            warningUI.color = warningColor;
            yield return null;
        }

        warningUI.gameObject.SetActive(false);
    }

    // 업그레이드 비용 계산 함수
    private int CalculateUpgradeCost(int level)
    {
        return 100 + (level * 200);
    }

    // 골드 UI 업데이트 함수
    public void UpdateGoldUI()
    {
        goldText.text = $"{GameManager.Instance.currency.gold}";
        jewel.text = $"{GameManager.Instance.currency.crystal}";
    }

    // 적 HP UI 업데이트 함수
    public void UpdateEnemyHpUI(float maxHp, float curHp)
    {
        curHp = Mathf.Round(curHp * 10) / 10;
        monsterHp.fillAmount = curHp / maxHp;
        monsterHpTxt.text = $"{curHp}";
    }

    public void UpdateEnemyNameUI(string name)
    {
        monsterName.text = $"{name}";
    }

    //플레이어 HP UI 업데이트 함수
    public void UpdatePlayerHPUI(float maxHp, float curHp)
    {
        curHp = Mathf.Round(curHp * 10) / 10;
        playerHpBar.fillAmount = curHp / maxHp;
        playerHptext.text = $"{curHp}";
    }
}

// 스탯 타입 정의
public enum StatType
{
    HP,
    Attack,
    Defense,
    Speed,
    CriticalRate,
    CriticalDamage
}

// 스탯 클래스 정의
[System.Serializable]
public class Stat
{
    public TextMeshProUGUI statLv; // 스탯 레벨 텍스트
    public TextMeshProUGUI statValue; // 스탯 값 텍스트
    public TextMeshProUGUI lvPrice; // 레벨업 가격 텍스트
    public Button lvUpBtn; // 레벨업 버튼
    public int level; // 현재 레벨
    public float baseValue; // 기본 값
    public StatType statType; // 스탯 타입
}