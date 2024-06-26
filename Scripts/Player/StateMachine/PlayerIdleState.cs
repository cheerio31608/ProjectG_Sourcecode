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

        //���� ����� �Ÿ� ���� ������ AttackState�� ����
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
