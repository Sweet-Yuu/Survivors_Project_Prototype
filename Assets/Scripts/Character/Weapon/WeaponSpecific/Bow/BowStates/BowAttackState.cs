using UnityEngine;

public class BowAttackState : BowActivityState
{
   public bool isAnimationFinished { get; set; }

    public BowAttackState(
        Bow bow,
        BowStateMachine stateMachine,
        BowData bowData,
        string animBoolName
    ) : base(bow, stateMachine, bowData, animBoolName)
    {
    }
    

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        bow.BowAttack.ResetCooldown();
        if (Player.Instance != null)
        {
            PlayerStats stats = Player.Instance.GetComponent<PlayerStats>();
            if (stats != null)
            {
              
                bow.Anim.SetFloat("animSpeed", stats.spd);
            }
            else
            {
                bow.Anim.SetFloat("animSpeed", Player.Instance.PlayerData.attackSpd);
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        bow.BowAttack.EndAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(bow.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}