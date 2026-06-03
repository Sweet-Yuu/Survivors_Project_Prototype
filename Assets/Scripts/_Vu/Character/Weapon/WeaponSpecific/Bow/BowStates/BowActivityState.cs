using UnityEngine;

public class BowActivityState : BowState
{
    public BowActivityState(Bow bow, BowStateMachine stateMachine, BowData bowData, string animBoolName) : base(bow, stateMachine, bowData, animBoolName)
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
        Vector2 mouserDir = Player.Instance.InputHandler.MouseDirection.normalized;
        if (mouserDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(mouserDir.y, mouserDir.x) * Mathf.Rad2Deg;
            bow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
