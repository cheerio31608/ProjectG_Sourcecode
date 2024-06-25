using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private int drawNum;
    private int resultNum;

    [SerializeField] private int forThePlayer;
    [SerializeField] private float delayTime;   // UI delay
    [SerializeField] private int drawCost; // 뽑기 비용

    // 뽑기 버튼
    public Button DrawBtn;
    public Button ConfirmBtn;

    public GameObject ResultUI;

    public int[] resultPct;

    [SerializeField] private GameObject[] resultObjects = new GameObject[10];

    [SerializeField] private Sprite[] resultImages = new Sprite[8];

    [SerializeField] private TextMeshProUGUI[] PctText;

    public ItemStat itemStat;

    // 아이템 뽑기 확률
    [field: Header("Draw")]
    [field: SerializeField] private int extraPct;
    [field: SerializeField] private int voidParasitePct;
    [field: SerializeField] private int magicPendantPct;
    [field: SerializeField] private int hellPendantPct;
    [field: SerializeField] private int blessingOfGoldPct;
    [field: SerializeField] private int hellFireSkullPct;
    [field: SerializeField] private int windBladePct;
    [field: SerializeField] private int darkBladePct;
    [field: SerializeField] private int eternalShadowPct;

    [field: SerializeField] private int drawCount = 10;

    [field: Header("ExtraGoldRange")]
    [field: SerializeField] private int minRange = 100;
    [field: SerializeField] private int maxRange = 501;

    // Start is called before the first frame update
    void Start()
    {
        itemCountConversion();
        ConvertPctToText();

        DrawBtn.onClick.AddListener(Draw);
        ConfirmBtn.onClick.AddListener(CloseResultUI);

        ResultUI.SetActive(false);

    }

    // 뽑기 확률
    public void GenerateRandomNum()
    {
        int x;
        int drawNumUpdate = 0;

        if (itemStat.ItemCounts[7] == 1 && drawNum < drawNumUpdate + forThePlayer)
        {
            x = Random.Range(1, 100);
        }
        else
        {
            x = Random.Range(1, 101);
        }

        //int x = Random.Range(1, 101);

        if (x <= extraPct)
        {
            int extra = Random.Range(minRange, maxRange);
            GameManager.Instance.currency.gold += extra;

            resultNum = itemStat.ItemCounts.Length;

            return;
        }
        else
        {
            for (int i = 0; i < itemStat.ItemCounts.Length; i++)
            {
                if (x <= resultPct[i])
                {
                    if(i == 7)
                    {
                        drawNumUpdate = drawNum;
                        Debug.Log(drawNumUpdate);
                    }
                    itemStat.ItemCounts[i]++;
                    itemStat.AddItemStatToPlayerStat(i);
                    resultNum = i;
                    break;
                }
            }
        }
    }

    public void Draw()
    {

        if (GameManager.Instance.currency.crystal < drawCost)
        {
            Debug.Log("Not enough Crystal");
            return;
        }

        StartCoroutine(UIDelay(ResultUI, delayTime, true));
        DrawBtn.interactable = false;

        GameManager.Instance.currency.crystal -= drawCost;
        UIManager.Instance.UpdateGoldUI();

        for (int i = 0; i < drawCount; i++)
        {
            GenerateRandomNum();
            Image Image = resultObjects[i].GetComponent<Image>();
            Image.sprite = resultImages[resultNum];
        }

        drawNum++;
        Debug.Log(drawNum);
    }

    public void CloseResultUI()
    {
        StartCoroutine(UIDelay(ResultUI, delayTime, false));
        DrawBtn.interactable = true;
    }

    // UI delay, 버튼 클릭 코루틴 애니메이션을 보기 위한 딜레이
    IEnumerator UIDelay(GameObject gameObject, float delay, bool active)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(active);
    }

    // 아이템 확률 택스트에 반영
    private void ConvertPctToText()
    {
        for (int i = 0; i < PctText.Length; i++)
        {
            if (i == 0)
            {
                PctText[i].text = $"{(resultPct[i] - extraPct).ToString()} %";
            }
            else
            {
                PctText[i].text = $"{(resultPct[i] - resultPct[i-1]).ToString()} %";
            }
        }
    }

    private void itemCountConversion()
    {
        resultPct[0] = voidParasitePct;
        resultPct[1] = magicPendantPct;
        resultPct[2] = hellPendantPct;
        resultPct[3] = blessingOfGoldPct;
        resultPct[4] = hellFireSkullPct;
        resultPct[5] = windBladePct;
        resultPct[6] = darkBladePct;
        resultPct[7] = eternalShadowPct;
    }
}
