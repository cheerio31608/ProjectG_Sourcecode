using UnityEngine;

public class PlayerDieState : PlayerBaseState
{
    
    
    public PlayerDieState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.player.Animator.SetTrigger(stateMachine.player.AnimationData.DieParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
