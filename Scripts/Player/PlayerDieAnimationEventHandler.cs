using UnityEngine;

public class PlayerDieAnimationEventHandler: MonoBehaviour
{
    public void PlayerDie()
    {
        enabled = false;
        GameManager.Instance.StageSystem.PlayerDieEvent.Invoke();
        Destroy(GetComponentInParent<Player>().gameObject);
    }
}
