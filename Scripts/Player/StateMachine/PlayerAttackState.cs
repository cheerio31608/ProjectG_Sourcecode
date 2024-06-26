public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //공격이 끝나면, IdleState로 전환
        if (IsDestroy())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
