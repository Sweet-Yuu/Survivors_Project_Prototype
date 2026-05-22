using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public EnemyMoveState MoveState { get; private set; }
    public EnemyHurtState HurtState { get; private set; }
    public EnemyDieState DieState { get; private set; }

    public EnemyStateMachine StateMachine { get; private set; }

    public EnemyHealth Health { get; private set; }

    public EnemyVisual Visual { get; private set; }
    public Rigidbody2D RB { get; private set; }


    public static Enemy Instance { get; private set; }
    [SerializeField] private EnemyData enemyData;
    public EnemyData EnemyData => enemyData;

   

    protected virtual void Awake()
    {
        Instance = this;
        Visual = GetComponentInChildren<EnemyVisual>();
        RB = GetComponent<Rigidbody2D>();

        Health = GetComponent<EnemyHealth>();

        StateMachine = new EnemyStateMachine();

        MoveState = new EnemyMoveState(this, StateMachine, EnemyData, "move");
        HurtState = new EnemyHurtState(this, StateMachine, EnemyData, "hurt");
        DieState = new EnemyDieState(this, StateMachine, EnemyData, "die");
    }
    protected virtual void Start()
    {
        StateMachine.Initialize(MoveState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    
}