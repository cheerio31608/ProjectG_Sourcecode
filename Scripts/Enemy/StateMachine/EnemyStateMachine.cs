using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public GameObject Target { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }

    public float MovementSpeed { get; set; }


    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);

        // 데이터 할당
        MovementSpeed = 1.0f;
    }
}