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

        //������ ������, IdleState�� ��ȯ
        if (IsDestroy())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
