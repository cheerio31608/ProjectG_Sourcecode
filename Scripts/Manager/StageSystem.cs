using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSystem : MonoBehaviour
{
    public static StageSystem Instance;
    public StageData StageData;
    public GameObject[] EnemyPrefabs;
    [HideInInspector] public GameObject SpawnedEnemy;   // 현재 스폰되어있는 적

    public GameObject PlayerPrefab;

    public TextMeshProUGUI FloorNumText;
    public TextMeshProUGUI StageResultText;
    public TextMeshProUGUI EnemyHPText;
    public Image EnemyHpBar;

    public Action StageClearEvent;  // 스테이지 클리어 이벤트
    public Action PlayerDieEvent;   // 스테이지 실패(플레이어 사망) 이벤트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SetStageClearEvent();
        SetPlayerDieEvent();
        InitStage();
    }

    private void SetStageClearEvent()
    {
        StageClearEvent += StageClear;
    }

    private void SetPlayerDieEvent()
    {
        PlayerDieEvent += PlayerDie;
    }

    // 몬스터 사망 시 코루틴
    private void StageClear()
    {
        StartCoroutine(StageClearCoroutine());
    }

    private IEnumerator StageClearCoroutine()
    {
        ShowStageClearText();
        yield return new WaitForSeconds(2f);
        HideStageResultText();
        ChangeStage();
        InitStage();
    }

    // 플레이어 사망 시 코루틴
    private void PlayerDie()
    {
        StartCoroutine(PlayerDIeCoroutine());
    }

    private IEnumerator PlayerDIeCoroutine()
    {
        ShowPlayerDiedText();
        yield return new WaitForSeconds(2f);
        HideStageResultText();
        ChangeStage(StageData.StageNum);
        InitStage();
    }

    // 스테이지 시작 시 초기설정
    private void InitStage()
    {
        FloorNumText.text = $"{StageData.StageNum}";
        
        SpawnPlayer();
        SpawnEnemy(StageData.StageNum % 13);
    }

    // 플레이어 생성 메서드
    private void SpawnPlayer()
    {
        if (GameManager.Instance.Player == null)
        {
            GameObject Player = Instantiate(PlayerPrefab, new Vector3(-0.949f, 214.018f), transform.rotation);
            GameManager.Instance.Player = Player.GetComponent<Player>();
            UIManager.Instance.player = Player.GetComponent<Player>();
        }
    }

    // 몬스터 생성 메서드
    private void SpawnEnemy(int index)
    {
        if (SpawnedEnemy != null)
            Destroy(SpawnedEnemy);
        SpawnedEnemy = Instantiate(EnemyPrefabs[index]);
    }

    // 스테이지 전환 메서드
    public void ChangeStage()
    {
        ChangeStage(++StageData.StageNum);
    }

    public void ChangeStage(int stageNum)
    {
        StageData.StageNum = stageNum;
    }

    // 스테이지 종료 시 결과 텍스트 출력하는 메서드
    private void ShowStageClearText()
    {
        StageResultText.text = "Victory!";
        StageResultText.gameObject.SetActive(true);
    }

    private void ShowPlayerDiedText()
    {
        StageResultText.text = "Defeat!";
        StageResultText.gameObject.SetActive(true);
    }

    private void HideStageResultText()
    {
        StageResultText.gameObject.SetActive(false);
    }
}
