using System.Collections;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    
    public PlayerDieState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Player.IsDead = true;

        player.RB.linearVelocity = Vector2.zero;

        player.Anim.SetTrigger("die");

        
        player.InputHandler.enabled = false;
       

        GameManager.Instance.ClearAllEnemies();
        player.StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3.017f);

        GameOverManager.Instance.GameOver();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
