using UnityEngine;

public class BowIdleState : BowActivityState
{
    public BowIdleState(Bow bow, BowStateMachine stateMachine, BowData bowData, string animBoolName) : base(bow, stateMachine, bowData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        bool attackInput = Player.Instance.InputHandler.AttackInput;

        

        if (attackInput && bow.BowAttack.CanAttack)
        {
            stateMachine.ChangeState(bow.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
