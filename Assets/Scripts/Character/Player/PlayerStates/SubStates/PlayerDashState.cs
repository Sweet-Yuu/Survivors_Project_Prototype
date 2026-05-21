using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
    private int originalLayer;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Dash.dashStartTime = Time.time;
        player.Dash.lastDashTime = Time.time;

        player.Dash.dashDirection = player.InputHandler.movementInput;
        if (player.Dash.dashDirection == Vector2.zero)
        {
            player.Dash.dashDirection = player.LastInput.normalized;
        }
        

        player.Anim.SetFloat("dashX", player.Dash.dashDirection.x);
        player.Anim.SetFloat("dashY", player.Dash.dashDirection.y);

        player.IsInvincible(true);
        originalLayer = player.gameObject.layer;
        player.gameObject.layer = LayerMask.NameToLayer("DashLayer");
    }

    public override void Exit()
    {
        base.Exit();
        
        player.RB.linearVelocity = Vector2.zero;
        player.IsInvincible(false);
        player.gameObject.layer = originalLayer;
    }

    public override void LogicUpdate()
    {
        //base.LogicUpdate();
        if (Time.time >= player.Dash.dashStartTime + Player.Instance.PlayerData.dashDuration)
        {
            
            if (input != Vector2.zero)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }



    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.RB.linearVelocity = player.Dash.dashDirection * Player.Instance.PlayerData.dashSpeed;
    }
}
