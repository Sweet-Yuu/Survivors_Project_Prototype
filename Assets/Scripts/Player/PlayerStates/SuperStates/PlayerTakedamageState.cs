using UnityEngine;
using UnityEngine.UIElements;

public class PlayerTakedamageState : PlayerState
{
    private float knockbackDuration = 0.3f;
    private float knockbackSpeed = 5f;
    private float hurtStartTime;
    public PlayerTakedamageState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        hurtStartTime = Time.time;

        player.Anim.SetFloat("hitX", player.Health.HitDirection.x);
        player.Anim.SetFloat("hitY", player.Health.HitDirection.y);

        player.Anim.SetTrigger("hurt");
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.linearVelocity = Vector2.zero;
        player.Anim.ResetTrigger("hurt");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= hurtStartTime + knockbackDuration)
        {
            if (player.InputHandler.movementInput != Vector2.zero)
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
        player.RB.linearVelocity = player.Health.HitDirection * knockbackSpeed;
    }
}
