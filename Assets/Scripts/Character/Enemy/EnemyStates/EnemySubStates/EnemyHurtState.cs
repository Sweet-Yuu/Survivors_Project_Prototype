using UnityEngine;

public class EnemyHurtState : EnemyActivityState
{
    private float knockbackDuration = 0.1f;
    private float knockbackSpeed = 2f;
    private float hurtStartTime;
    public EnemyHurtState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        
    }
   

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        RestartLogic();
    }
    private void RestartLogic()
    {
        hurtStartTime = Time.time; 

        enemy.Visual.Anim.SetFloat("hitX", -enemy.Health.HitDirection.x);
        enemy.Visual.Anim.SetFloat("hitY", -enemy.Health.HitDirection.y);

        enemy.Visual.Anim.SetTrigger("hurt");
    }
    public void ForceRestartHurt()
    {
        RestartLogic();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.RB.linearVelocity = Vector2.zero;
        enemy.Visual.Anim.ResetTrigger("hurt");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= hurtStartTime + knockbackDuration)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.RB.linearVelocity = enemy.Health.HitDirection * knockbackSpeed;
    }
}
