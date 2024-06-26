using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; }
    public GameObject Target { get; set; }
    public PlayerIdleState IdleState { get; }
    public PlayerAttackState AttackState { get; }
    public PlayerDieState DieState { get; }

    public PlayerStateMachine(Player player)
    {
        this.player = player;
        IdleState = new PlayerIdleState(this);
        AttackState = new PlayerAttackState(this);
        DieState = new PlayerDieState(this);
    }
}
