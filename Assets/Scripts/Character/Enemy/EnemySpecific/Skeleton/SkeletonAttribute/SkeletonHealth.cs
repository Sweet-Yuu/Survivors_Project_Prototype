using UnityEngine;


public class SkeletonHealth : EnemyHealth
{
    private Skeleton skeleton;

    protected override void Awake()
    {
        base.Awake();
        skeleton = GetComponent<Skeleton>();
    }

    protected override void Start()
    {
        base.Start(); 
    }

    protected override void TriggerHurt()
    {
        if (skeleton == null || skeleton.StateMachine == null) return;

        if (skeleton.StateMachine.CurrentState == skeleton.skeletonHurtState)
        {
            skeleton.skeletonHurtState.ForceRestartHurt();
        }
        else
        {
            skeleton.StateMachine.ChangeState(skeleton.skeletonHurtState);
        }
    }

 
    protected override void Die()
    {
        if (skeleton == null || skeleton.StateMachine == null) return;

        skeleton.StateMachine.ChangeState(skeleton.DieState);
    }
}