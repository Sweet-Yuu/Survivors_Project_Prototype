using UnityEngine;

public class PlayerTakedamageState : PlayerState
{
    public float hurtTime;
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
        hurtTime = 0.2f;
        
        player.Anim.SetTrigger("hurt");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        hurtTime -= Time.deltaTime;

        if (hurtTime <= 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
