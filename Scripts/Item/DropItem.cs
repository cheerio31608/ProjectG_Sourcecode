using UnityEngine;

public class DropItem : MonoBehaviour
{
    private Animator animator;
    private int dropAnimationHash = Animator.StringToHash("Drop");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // 생성과 동시에 애니메이션 재생
    private void Start()
    {
        animator.Play(dropAnimationHash);
    }
}
