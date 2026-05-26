using UnityEngine;

public class SkeletonState : EnemyState
{
    public Skeleton skeleton;
    public SkeletonData skeletonData;
    public SkeletonState(Skeleton skeleton, EnemyStateMachine stateMachine, SkeletonData skeletonData, string animBoolName)
        : base(skeleton, stateMachine, skeletonData, animBoolName)
    {
        // Store the skeleton-specific references for use in your unique skeleton states
        this.skeleton = skeleton;
        this.skeletonData = skeletonData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
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
