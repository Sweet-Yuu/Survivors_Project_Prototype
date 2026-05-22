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

        player.Movement.Stop();

        player.Anim.SetTrigger("die");
        
        Enemy.Instance.RB.linearVelocity = Vector2.zero;
        player.InputHandler.enabled = false;
        if (player.bow != null)
        {
            player.bow.GetComponent<SpriteRenderer>().enabled = false;
        }


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
