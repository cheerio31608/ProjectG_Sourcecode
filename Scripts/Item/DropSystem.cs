using System;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    public Action<Vector3> MonsterDropEvent;   // 일반 몬스터 드랍 이벤트
    public Action<Vector3> BossDropEvent;      // 보스 드랍 이벤트

    public GameObject GoldPrefab;
    public GameObject CrystalPrefab;

    private int gold;
    private int crystal;

    private void Start()
    {
        MonsterDropEvent += DropGold;
        BossDropEvent += DropGold;
        BossDropEvent += DropCrystal;
    }

    public void InvokeMonsterDropEvent(Vector3 position)
    {
        MonsterDropEvent.Invoke(position);
    }

    public void InvokeBossDropEvent(Vector3 position)
    {
        BossDropEvent.Invoke(position);
    }

    // 골드 획득 메서드 (주로 시각적 효과)
    // Enemy에서 OnDie 이벤트 발생 시 호출
    // OnDie(이벤트) -> InvokeMonster(Boss)DropEvent -> Monster(Boss)DropEvent(이벤트) -> DropGold 순서
    private void DropGold(Vector3 position)
    {
        Instantiate(GoldPrefab, position + new Vector3(-0.2f, 0), transform.rotation);
    }

    // 골드 획득 메서드 (골드량 실수치 증가)
    // DropAnimationHandler에서 애니메이션 이벤트로 호출
    public void GetGold()
    {
        // TODO : 골드량 설정하기
        GetGold(GameManager.Instance.StageSystem.StageData.StageNum * 100);
    }

    public void GetGold(int amount)
    {
        GameManager.Instance.currency.gold += amount;
        UIManager.Instance.UpdateGoldUI();
    }

    // 보석 획득 메서드 (주로 시각적 효과)
    // Enemy에서 OnDie 이벤트 발생 시 호출
    // OnDie(이벤트) -> InvokeBossDropEvent -> BossDropEvent(이벤트) -> DropCrystal 순서
    private void DropCrystal(Vector3 position)
    {
        Instantiate(CrystalPrefab, position + new Vector3(0.2f, 0), transform.rotation);
    }

    // 보석 획득 메서드 (골드량 실수치 증가)
    // DropAnimationHandler에서 애니메이션 이벤트로 호출
    public void GetCrystal()
    {
        // TODO : 크리스탈량 설정하기
        GetCrystal(GameManager.Instance.StageSystem.StageData.StageNum * 100);
    }

    public void GetCrystal(int amount)
    {
        GameManager.Instance.currency.crystal += amount;
        UIManager.Instance.UpdateGoldUI();
    }
}
