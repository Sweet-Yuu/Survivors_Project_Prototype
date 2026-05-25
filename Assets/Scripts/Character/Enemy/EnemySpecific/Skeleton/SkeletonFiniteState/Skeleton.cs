using UnityEngine;

public class Skeleton : Enemy
{
    public SkeletonAttackState attackState { get; private set; }

    public SkeletonData SkeletonData => EnemyData as SkeletonData;
    protected override void Awake()
    {
        base.Awake();
        attackState=new SkeletonAttackState(this,this.StateMachine,SkeletonData,"attack");
    }
     protected override void Start()
    {
        base.Start();
    }
}
