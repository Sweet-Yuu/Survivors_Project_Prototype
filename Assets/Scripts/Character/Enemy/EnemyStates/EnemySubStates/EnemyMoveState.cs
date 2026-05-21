using UnityEngine;

public class EnemyMoveState : EnemyActivityState
{
    private Transform playerTransform;
    private float moveSpeed;
    private float attackRange;
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        moveSpeed = enemyData.moveSpeed;
        attackRange = enemyData.attackRange;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameObject player= GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.RB.linearVelocity = Vector2.zero;
        enemy.Visual.UpdateMovementBlendTree(0f, 0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (playerTransform == null)
        {
            // stateMachine.ChangeState(enemy.IdleState);
            return;
        }
        

        float distanceToPlayer = Vector2.Distance(enemy.transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            enemy.RB.linearVelocity = Vector2.zero;
            
            // stateMachine.ChangeState(enemy.AttackState);
            return;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
            enemy.RB.linearVelocity = direction * moveSpeed;
            if (enemy.RB.linearVelocity.magnitude > 0.1f)
            {
                enemy.Visual.UpdateMovementBlendTree(enemy.RB.linearVelocity.x, enemy.RB.linearVelocity.y);
            }
        }
        else
        {
            enemy.RB.linearVelocity = Vector2.zero;
            enemy.Visual.UpdateMovementBlendTree(0f, 0f);
        }
    }
}
