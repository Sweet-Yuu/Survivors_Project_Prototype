using UnityEngine;

public class EnemyHurtState : EnemyActivityState
{
    private float stunDuration; 

    private float stunTimer;
    private Transform playerTransform;
    public EnemyHurtState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        stunDuration = enemyData.stunDuration;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        stunTimer = stunDuration;
        enemy.SetInvincible(true); // Make the enemy invincible while hurt
        enemy.RB.linearVelocity= Vector2.zero; // Stop enemy movement when hurt
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;

            
            Vector2 hitDirection = (playerTransform.position - enemy.transform.position).normalized;

           
            enemy.Visual.UpdateMovementBlendTree(hitDirection.x, hitDirection.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetInvincible(false); // Remove invincibility when exiting hurt state
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        stunTimer -= Time.deltaTime;
        if (stunTimer <= 0)
        {
            if (enemy is Enemy other)
            {
                stateMachine.ChangeState(other.MoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.RB.linearVelocity = Vector2.zero; // Ensure the enemy remains stationary while hurt
    }
}
