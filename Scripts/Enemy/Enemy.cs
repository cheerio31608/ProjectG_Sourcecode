using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("Reference")]

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D _rigidbody { get; private set; }
    public EnemyHealth health { get; private set; }

    private EnemyStateMachine stateMachine;
    private Vector3 position;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        health = GetComponent<EnemyHealth>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
        health.OnDie += OnDie;
        GameManager.Instance.Player.stateMachine.Target = gameObject;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");
        position = transform.position;
        if (GameManager.Instance.StageSystem.StageData.IsBossStage)
            GameManager.Instance.DropSystem.InvokeBossDropEvent(position);
        else
            GameManager.Instance.DropSystem.InvokeMonsterDropEvent(position);
        enabled = false;
        StartCoroutine(DestroyAfterDelay(0.8f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
