using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public int gold;
    public int crystal;

    // test용
    [SerializeField] private int price = 10;

    // public Button UpgradeBtn;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;


    // Start is called before the first frame update
    void Start()
    {
        // Player 싱글톤에서 Currency를 선언하고 넘겨준다.
        // Player.Instance.currency = this;
        //gold = 5000;
        //crystal = 3000;

        //// Item 업그레이드 버튼
        //UpgradeBtn.onClick.AddListener(Upgrade);
        //UpgradeBtn.interactable = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateCurrencyUI();
    }

    // 재화 UI 업데이드
    public void UpdateCurrencyUI()
    {
        goldText.text = gold.ToString();
        crystalText.text = crystal.ToString();
    }

    // 강화 비용이 충분한지 확인
    // 스탯 강화할 때 if(IsBUpgradable == false) return; 추가
    public bool IsUpgradable(int cost)
    {
        if (cost > gold)
        {
            Debug.Log("골드 부족");
            return false;
        }
        else
            return true;
    }

    // 뽑기 비용이 충분한지 확인
    // 추후 리팩토링
    public bool IsDrawable(int cost)
    {
        if (cost > crystal)
            return false;
        else
            return true;
    }

    // 강화 비용(골드) 소모
    public void PurchaseUpgrade(int cost)
    {
        gold -= cost;
    }

    // Test용 함수
    public void Upgrade()
    {
        int cost = price;
        if (!IsUpgradable(cost))
            return;
        PurchaseUpgrade(cost);
    }
}
