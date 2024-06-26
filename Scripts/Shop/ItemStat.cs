using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemStat : MonoBehaviour
{
    private UnityAction UnLockMessage;

    private bool[] isUnLock;

    //public float HP { get; set; }
    //public float Attack { get; set; }
    //public float AttackSpeed { get; set; }
    //public float CriticalRate { get; set; }
    //public float CriticalDamage { get; set; }
    //public float Defence { get; set; }

    public int eternalShadowCount = 0;
    public int blessingOfGoldCount = 0;
    public int darkBladeCount = 0;
    public int hellPendantCount = 0;
    public int windBladeCount = 0;
    public int magicPendantCount = 0;
    public int hellFireSkullCount = 0;
    public int voidParasiteCount = 0;

    Color color;

    public GameObject UnLockMessageUI;

    public TextMeshProUGUI UnLockMessageText;

    public Button ConfirmBtn;

    public int[] ItemCounts;

    public Button[] ItemButtons;

    public Image[] ItemImages;

    public TextMeshProUGUI[] ItemStatText;

    public float[] ItemStats;

    private Currency currency;
    private PlayerStat playerStat;

    //[field: Header("Item Stat")]
    //[field: SerializeField] private float voidParasiteStat;
    //[field: SerializeField] private float magicPendantStat;
    //[field: SerializeField] private float hellPendantStat;
    //[field: SerializeField] private float blessingOfGoldStat;
    //[field: SerializeField] private float hellFireSkullStat;
    //[field: SerializeField] private float windBladeStat;
    //[field: SerializeField] private float darkBladeStat;
    //[field: SerializeField] private float eternalShadowStat;

    [field: SerializeField] private string[] ItemNames;

    private void Start()
    {
        // 이미지 투명도를 낮춰 장금 효과 표현
        for (int i = 0; i < ItemImages.Length; i++)
        {
            SetTransparency(100f, i);
        }

        isUnLock = new bool[8];
        for (int i = 0; i < isUnLock.Length; i++)
        {
            isUnLock[i] = false;
        }

        //for (int i = 0; i < ItemButtons.Length; i++)
        //{
        //    UnLockMessage = () => { UnLockItem(ItemButtons[i], i); };
        //    ItemButtons[i].onClick.AddListener(UnLockMessage);
        //    Debug.Log("ff");
        //}

        UnLockMessage = () => { UnLockItem(ItemButtons[0], 0); };
        ItemButtons[0].onClick.AddListener(UnLockMessage);
        UnLockMessage = () => { UnLockItem(ItemButtons[1], 1); };
        ItemButtons[1].onClick.AddListener(UnLockMessage);
        UnLockMessage = () => { UnLockItem(ItemButtons[2], 2); };
        ItemButtons[2].onClick.AddListener(UnLockMessage);
        UnLockMessage = () => { UnLockItem(ItemButtons[3], 3); };
        ItemButtons[3].onClick.AddListener(UnLockMessage);
        UnLockMessage = () => { UnLockItem(ItemButtons[4], 4); };
        ItemButtons[4].onClick.AddListener(UnLockMessage);
        UnLockMessage = () => { UnLockItem(ItemButtons[5], 5); };
        ItemButtons[5].onClick.AddListener(UnLockMessage);
        UnLockMessage = () => { UnLockItem(ItemButtons[6], 6); };
        ItemButtons[6].onClick.AddListener(UnLockMessage);
        UnLockMessage = () => { UnLockItem(ItemButtons[7], 7); };
        ItemButtons[7].onClick.AddListener(UnLockMessage);

        //ItemButtons[0].onClick.AddListener(() => UnLockItem(ItemButtons[0], 0));
        //ItemButtons[1].onClick.AddListener(() => UnLockItem(ItemButtons[1], 1));
        //ItemButtons[2].onClick.AddListener(() => UnLockItem(ItemButtons[2], 2));
        //ItemButtons[3].onClick.AddListener(() => UnLockItem(ItemButtons[3], 3));
        //ItemButtons[4].onClick.AddListener(() => UnLockItem(ItemButtons[4], 4));
        //ItemButtons[5].onClick.AddListener(() => UnLockItem(ItemButtons[5], 5));
        //ItemButtons[6].onClick.AddListener(() => UnLockItem(ItemButtons[6], 6));
        //ItemButtons[7].onClick.AddListener(() => UnLockItem(ItemButtons[7], 7));

        ConfirmBtn.onClick.AddListener(CloseUnLockMessageUI);

        currency = FindObjectOfType<Currency>();

        ConvertItemNames();
        ShowItemStat();
    }

    private void Update()
    {
        ConvertItemCount();
    }

    public void SetTransparency(float alpha, int index)
    {
        color = ItemImages[index].color;
        color.a = alpha / 255f;
        ItemImages[index].color = color;
    }

    private void CloseUnLockMessageUI()
    {
        UnLockMessageUI.SetActive(false);
    }


    public void UnLockItem(Button button, int index)
    {

        if (ItemCounts[index] == 0)
        {
            UnLockMessageUI.SetActive(true);
            UnLockMessageText.text = $"{ItemNames[index]} \n장금 해제 불가!";
        }
        else
        {
            SetTransparency(255f, index);
            //UnLockMessageText.text = $"{ItemNames[index]} \n장금 해제!";
            isUnLock[index] = true;
            for(int i=0; i < ItemCounts[index]; i++)
            {
                AddItemStatToPlayerStat(index);
            }
            ItemButtons[index].interactable = false;
            //button.onClick.RemoveListener(UnLockMessage);
        }
    }

    // TODO: itemCounts[]에 할당된 값 설명
    public void ConvertItemCount()
    {
        voidParasiteCount = ItemCounts[0];
        magicPendantCount = ItemCounts[1];
        hellPendantCount = ItemCounts[2];
        blessingOfGoldCount = ItemCounts[3];
        hellFireSkullCount = ItemCounts[4];
        windBladeCount = ItemCounts[5];
        darkBladeCount = ItemCounts[6];
        eternalShadowCount = ItemCounts[7];
    }

    private void ConvertItemNames()
    {
        ItemNames[0] = "공허 기생충";
        ItemNames[1] = "마법 팬던트";
        ItemNames[2] = "지옥 팬던트";
        ItemNames[3] = "황금의 축복";
        ItemNames[4] = "지옥불 해골";
        ItemNames[5] = "윈드 블레이드";
        ItemNames[6] = "다크 블레이드";
        ItemNames[7] = "이터널 쉐도우";
    }

    private void ShowItemStat()
    {
        int i;

        for(i = 0; i < ItemStatText.Length - 1; i++)
        {
            ItemStatText[i].text = $"+ {ItemStats[i]}";
        }

        ItemStatText[7].text = $"+ {ItemStats[7]}%";
    }

    // 아이템 장금 해제하면 아이템 스탯을 플레이어 스탯에서 추가
    public void AddItemStatToPlayerStat(int index)
    {
        int[] ItemCountsUpdate = new int[8];
        if (isUnLock[index] == true && (ItemCountsUpdate[index] < ItemCounts[index]))
        {
            switch (index)
            {
                case 0:
                    GameManager.Instance.HP += ItemStats[0];
                    GameManager.Instance.MaxHP += ItemStats[0];
                    break;
                case 1:
                    GameManager.Instance.Defence += ItemStats[1];
                    break;
                case 2:
                    GameManager.Instance.CriticalRate += ItemStats[2];
                    break;
                case 3:
                    // TODO: PlayerStat에 골드 획득량 데이터 추가
                    // 골드 획득량 default 값을 float 1f로 설정
                    break;
                case 4:
                    GameManager.Instance.CriticalDamage += ItemStats[4];
                    break;
                case 5:
                    GameManager.Instance.AttackSpeed += ItemStats[5];
                    break;
                case 6:
                    GameManager.Instance.Attack += ItemStats[6];
                    break;
                case 7:
                    GameManager.Instance.HP *= (1 + ItemStats[7]);
                    GameManager.Instance.Attack *= (1 + ItemStats[7]);
                    GameManager.Instance.AttackSpeed *= (1 + ItemStats[7]);
                    GameManager.Instance.Defence *= (1 + ItemStats[7]);
                    GameManager.Instance.CriticalRate *= (1 + ItemStats[7]);
                    GameManager.Instance.CriticalDamage *= (1 + ItemStats[7]);
                    break;
            }
            ItemCountsUpdate[index]++;
            UIManager.Instance.UpdateStatData();
            UIManager.Instance.UpdatePlayerStat();
        }




    }

    //public void InitStat()
    //{
    //    HP = playerStat.PlayerHP;
    //    Attack = playerStat.PlayerAttack;
    //    AttackSpeed = playerStat.PlayerSpeed;
    //    CriticalRate = playerStat.CriticalRate;
    //    CriticalDamage = playerStat.CriticalDamage;
    //    Defence = playerStat.PlayerDefense;
    //}
}