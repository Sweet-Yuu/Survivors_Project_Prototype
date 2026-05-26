using UnityEngine;

public class SkeletonHurtState : SkeletonState
{
    private float knockbackDuration = 0.3f;
    private float knockbackSpeed = 2f;
    private float hurtStartTime;

    public SkeletonHurtState(Skeleton skeleton, EnemyStateMachine stateMachine, SkeletonData skeletonData, string animBoolName)
        : base(skeleton, stateMachine, skeletonData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        RestartLogic();
    }

    private void RestartLogic()
    {
        hurtStartTime = Time.time;

        
        if (skeleton.anim != null)
        {
            skeleton.anim.SetTrigger("hurt");
        }

       
    }

    public void ForceRestartHurt()
    {
        RestartLogic();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
        if (Time.time >= hurtStartTime + knockbackDuration)
        {
            stateMachine.ChangeState(skeleton.skeletonMoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
      
        skeleton.RB.linearVelocity = skeleton.Health.HitDirection * knockbackSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.RB.linearVelocity = Vector2.zero;

        if (skeleton.anim!= null)
        {
            skeleton.anim.ResetTrigger("hurt");
        }
    }
}