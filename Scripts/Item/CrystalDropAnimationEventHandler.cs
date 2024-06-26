using UnityEngine;

public class CrystalDropAnimationEventHandler : MonoBehaviour, IDropEvent
{
    // Drop 애니메이션에서 이벤트로 호출
    public void Drop()
    {
        GameManager.Instance.DropSystem.GetCrystal();
        Destroy(GetComponentInParent<DropItem>().gameObject);
    }
}
