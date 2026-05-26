using UnityEngine;

public class SkeletonAttackState : SkeletonState
{
    private Transform playerTransform;
    private float attackRange;
    private float lastAttackTime;

    
    private float attackCooldown = 1.2f;

    private float attackDuration = 0.717f;

    private bool isAnimationFinished;

    public SkeletonAttackState(Skeleton skeleton, EnemyStateMachine stateMachine, SkeletonData skeletonData, string animBoolName)
        : base(skeleton, stateMachine, skeletonData, animBoolName)
    {
        
        attackRange = skeletonData.attackRange;
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.RB.linearVelocity = Vector2.zero;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        ExecuteSingleAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (playerTransform == null)
        {
            stateMachine.ChangeState(skeleton.skeletonMoveState);
            return;
        }

        
        Vector2 direction = (playerTransform.position - skeleton.transform.position).normalized;
        if (direction.x > 0 && skeleton.facingDirection == -1) skeleton.Flip();
        else if (direction.x < 0 && skeleton.facingDirection == 1) skeleton.Flip();

    
        if (!isAnimationFinished && Time.time >= lastAttackTime + attackDuration)
        {
            isAnimationFinished = true;
          
            if (skeleton.anim != null)
            {
                skeleton.anim.ResetTrigger("attack");
            }
        }

        float distanceToPlayer = Vector2.Distance(skeleton.transform.position, playerTransform.position);

        
        if (distanceToPlayer > attackRange && isAnimationFinished)
        {
            stateMachine.ChangeState(skeleton.skeletonMoveState);
            return;
        }

        
        if (isAnimationFinished && Time.time >= lastAttackTime + attackCooldown)
        {
            ExecuteSingleAttack();
        }
    }

    private void ExecuteSingleAttack()
    {
        lastAttackTime = Time.time;
        isAnimationFinished = false;

        if (skeleton.anim != null)
        {
            skeleton.anim.SetTrigger("attack");
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        skeleton.RB.linearVelocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
        if (skeleton.anim != null)
        {
            skeleton.anim.ResetTrigger("attack");
        }
    }
}