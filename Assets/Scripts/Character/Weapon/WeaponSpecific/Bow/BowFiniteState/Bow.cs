using UnityEngine;

public class Bow : MonoBehaviour
{
    public BowAttack BowAttack { get; private set; }
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;

    public BowStateMachine StateMachine { get; private set; }

    public BowAttackState AttackState { get; private set; }
    public BowIdleState IdleState { get; private set; }

    public Animator Anim { get; private set; }
    private SpriteRenderer spriteRenderer;




    [SerializeField] private BowData bowData;
    public BowData BowData => bowData;

    private void Awake()
    {

        Anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        BowAttack = new BowAttack(this, arrowPrefab, firePoint);

        StateMachine = new BowStateMachine();

        AttackState = new BowAttackState(this, StateMachine, bowData, "attack");
        IdleState = new BowIdleState(this, StateMachine, bowData, "idle");

    }
    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void ShootArrowTrigger()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            if (Player.Instance != null)
            {
                
                float damageToDeal = Player.Instance.CalculateOutputDamage();

               
                BowAttack.StartAttack(damageToDeal);
            }
            else
            {
               
                BowAttack.StartAttack(10f);
            }
        }
    }
    public void AnimationFinishTrigger()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            AttackState.isAnimationFinished = true;
        }
    }
    
}