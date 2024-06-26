public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //공격 대상이 거리 내에 있으면 AttackState로 변경
        if(stateMachine.Target != null)
        {
            if (InAttackRange())
            {
                stateMachine.ChangeState(stateMachine.AttackState);
                return;
            }
        }       
    }
}
