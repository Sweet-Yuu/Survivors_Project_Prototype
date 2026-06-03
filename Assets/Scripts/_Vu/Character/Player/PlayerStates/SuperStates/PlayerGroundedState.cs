using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 input;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        input = player.InputHandler.movementInput;

        if (player.InputHandler.dashInput)
        {
            if (player.Dash.CanDash)
            {
                player.InputHandler.UseDashInput();
                stateMachine.ChangeState(player.DashState);
                return;
            }
            else
            {
                player.InputHandler.UseDashInput();
            }
            
        }
        Vector2 mouseDir =
        player.InputHandler.MouseDirection;

        player.Anim.SetFloat("lastInputX", mouseDir.x);
        player.Anim.SetFloat("lastInputY", mouseDir.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
