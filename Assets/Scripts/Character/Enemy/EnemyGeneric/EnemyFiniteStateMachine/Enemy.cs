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

    [SerializeField] private EnemyData enemyData;
    public EnemyData EnemyData => enemyData;

    [Header("Despawn Settings")]
    public float despawnDistance = 20f;
    Transform _player;

    protected virtual void Awake()
    {
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
        if (Player.Instance != null)
        {
            _player = Player.Instance.transform;
        }
        StateMachine.Initialize(MoveState);
    }

    private void Update()
    {
        if (_player == null) return;
        if (Vector2.Distance(transform.position, _player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }


    private void OnDestroy()
    {
        if (EnemySpawner.Instance != null)
        {
            EnemySpawner.Instance.OnEnemyKilled();
        }
    }

    void ReturnEnemy()
    {
        if (EnemySpawner.Instance == null || _player == null) return;
        var spawnPoints = EnemySpawner.Instance.relativeSpawnPoint;
        if (spawnPoints != null && spawnPoints.Count > 0)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            transform.position = _player.position + randomPoint.position;
        }
    }
}