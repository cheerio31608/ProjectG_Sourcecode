public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
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

    public virtual void Update()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    protected bool InAttackRange()
    {
        if (stateMachine.Target.transform.position.x - stateMachine.player.transform.position.x <= 1)
            return true;
        else
            return false;
    }

    protected bool IsDestroy()
    {
        if (stateMachine.Target == null)
            return true;
        else
            return false;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.player.Animator.SetBool(animationHash, false);
    }
}
