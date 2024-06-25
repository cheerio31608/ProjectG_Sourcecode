using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    private void Move()
    {
        if (stateMachine.Target != null && GameManager.Instance.Player != null)
        {
            Vector3 movementDirection = GetMovementDirection();
            Move(movementDirection);
        }
    }

    void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy._rigidbody.velocity = (movementDirection * movementSpeed);
    }

    protected void StopMove()
    {
        stateMachine.MovementSpeed = 0;
    }

    private Vector3 GetMovementDirection()
    {
        if (stateMachine.Target == null) return Vector3.zero;
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
        return dir;
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.Target != null && GameManager.Instance.Player != null)
        {
            if (stateMachine.Enemy.transform.position.x - stateMachine.Target.transform.position.x > 2.0f)
                return true;
        }
        return false;
    }

    protected bool InAttackRange()
    {
        if (stateMachine.Target != null && GameManager.Instance.Player != null)
        {
            if (stateMachine.Enemy.transform.position.x - stateMachine.Target.transform.position.x <= 1)
                return true;
        }
        return false;
    }

    protected bool IsDestroy()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            return true;
        else
            return false;
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed;
        return movementSpeed;
    }




}
