using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);

    }

    public override void Update()
    {
        base.Update();

        StopMove();

        //������ ������, IdleState�� ��ȯ
        if (IsDestroy())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

    }
}