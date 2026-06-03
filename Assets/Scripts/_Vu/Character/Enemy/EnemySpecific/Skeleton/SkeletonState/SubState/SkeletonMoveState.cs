using UnityEngine;

public class SkeletonMoveState : SkeletonState
{
    private Transform playerTransform;
    private float moveSpeed;
    private float attackRange;

    public SkeletonMoveState(Skeleton skeleton, EnemyStateMachine stateMachine, SkeletonData skeletonData, string animBoolName)
        : base(skeleton, stateMachine, skeletonData, animBoolName)
    {
        moveSpeed = skeletonData.moveSpeed;
        attackRange = skeletonData.attackRange;
    }

    public override void Enter()
    {
        base.Enter();

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (playerTransform == null) return;

       
        float distanceToPlayer = Vector2.Distance(skeleton.transform.position, playerTransform.position);

        
        if (distanceToPlayer <= attackRange)
        {
            skeleton.RB.linearVelocity = Vector2.zero;
            stateMachine.ChangeState(skeleton.skeletonAttackState); 
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (playerTransform != null)
        {
      
            Vector2 direction = (playerTransform.position - skeleton.transform.position).normalized;
            skeleton.RB.linearVelocity = direction * moveSpeed;

           
            if (direction.x > 0 && skeleton.facingDirection == -1)
            {
                skeleton.Flip();
            }
            else if (direction.x < 0 && skeleton.facingDirection == 1)
            {
                skeleton.Flip();
            }
        }
        else
        {
            skeleton.RB.linearVelocity = Vector2.zero;
        }
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.RB.linearVelocity = Vector2.zero;
    }
}