using UnityEngine;

public class Skeleton : Enemy
{
    public SkeletonHealth Health { get; private set; }
    public SkeletonMoveState skeletonMoveState { get; private set; }
    public SkeletonAttackState skeletonAttackState { get; private set; }
    public SkeletonHurtState skeletonHurtState { get; private set; }

    public SkeletonData SkeletonData => EnemyData as SkeletonData;
    public override void Awake()
    {
        base.Awake();
        Health = GetComponent<SkeletonHealth>();

        skeletonMoveState = new SkeletonMoveState(this, this.StateMachine, SkeletonData, "move");
        skeletonAttackState = new SkeletonAttackState(this,this.StateMachine,SkeletonData,"attack");
        skeletonHurtState = new SkeletonHurtState(this, this.StateMachine, SkeletonData, "hurt");
    }
    public override void Start()
    {
        base.Start();
        
        StateMachine.Initialize(skeletonMoveState);
    }
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void SetVelocity(float velocity)
    {
        base.SetVelocity(velocity);
    }

    public void TriggerDamageEvent()
    {
       
        Vector2 attackPosition = (Vector2)transform.position + new Vector2(facingDirection * 0.5f, 0f);

       
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPosition, SkeletonData.attackRange);

        foreach (Collider2D hit in hitObjects)
        {
            
            if (hit.CompareTag("Player"))
            {
                if (hit.TryGetComponent<IDamageable>(out var damageable))
                {
                   
                    damageable.TakeDamage(SkeletonData.damage);
                    return; 
                }
            }
        }

        
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Vector3 attackPosition = transform.position + new Vector3(facingDirection * 1f, 0f, 0f);

        if (SkeletonData != null)
        {
            Gizmos.DrawWireSphere(attackPosition, SkeletonData.attackRange);
        }
        else
        {
            Gizmos.DrawWireSphere(attackPosition, 1f);
        }
    }
}
