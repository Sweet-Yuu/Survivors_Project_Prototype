using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour,IDamageable
{
    public EnemyMoveState MoveState { get; private set; }
    public EnemyHurtState HurtState { get; private set; }

    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyVisual Visual { get; private set; }
    public Rigidbody2D RB { get; private set; }


    [SerializeField] private EnemyData enemyData;
    public EnemyData EnemyData => enemyData;

    public float CurrentHealth { get;private set; }
    public bool IsInvincible { get; private set; }

    protected virtual void Awake()
    {
        Visual = GetComponentInChildren<EnemyVisual>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine = new EnemyStateMachine();

        MoveState = new EnemyMoveState(this, StateMachine, EnemyData, "move");
        HurtState = new EnemyHurtState(this, StateMachine, EnemyData, "hurt");
    }
    protected virtual void Start()
    {
        StateMachine.Initialize(MoveState);
        CurrentHealth = EnemyData.maxHealth;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    public virtual void TakeDamage(float damage)
    {
        if (IsInvincible) return;

        
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, EnemyData.maxHealth);
        if (CurrentHealth <= 0)
        {
            //StateMachine.ChangeState(DieState);
            Destroy(gameObject); //test
        }
        else
        {
            StateMachine.ChangeState(HurtState);
        }
    }
    
    public void SetInvincible(bool value)
    {
        IsInvincible = value;
    }
}