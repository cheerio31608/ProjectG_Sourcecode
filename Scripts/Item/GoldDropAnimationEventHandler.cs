using UnityEngine;

public class GoldDropAnimationEventHandler : MonoBehaviour, IDropEvent
{
    // Drop 애니메이션에서 이벤트로 호출
    public void Drop()
    {
        GameManager.Instance.DropSystem.GetGold();
        Destroy(GetComponentInParent<DropItem>().gameObject);
    }
}
