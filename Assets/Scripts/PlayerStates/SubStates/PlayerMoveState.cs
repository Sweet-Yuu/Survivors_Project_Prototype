using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        Vector2 input = player.InputHandler.movementInput;

        player.Movement.Move(input);

        if (input != Vector2.zero)
        {
            player.LastInput = input;
        }

        player.Anim.SetFloat("InputX", input.x);
        player.Anim.SetFloat("InputY", input.y);

        player.Anim.SetFloat("lastInputX", player.LastInput.x);
        player.Anim.SetFloat("lastInputY", player.LastInput.y);

        player.Anim.SetFloat("speed", input.magnitude);

       

        if (input == Vector2.zero)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.Movement.Move(input);
        
    }
}
