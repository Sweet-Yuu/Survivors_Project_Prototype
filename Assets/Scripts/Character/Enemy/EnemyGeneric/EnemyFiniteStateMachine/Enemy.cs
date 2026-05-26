using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{

    public EnemyDieState DieState { get; private set; }

    public EnemyStateMachine StateMachine { get; private set; }

  

   public Animator anim {  get; private set; }  
    public Rigidbody2D RB { get; private set; }
    public GameObject AliveGo {  get; private set; }

    public int facingDirection{ get; private set; }

    public Vector2 velocityWorkspace;


    [SerializeField] private EnemyData enemyData;
    public EnemyData EnemyData => enemyData;

    [Header("Despawn Settings")]
    public float despawnDistance = 20f;
    Transform _player;

    public virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            AliveGo = anim.gameObject;
        }
        else
        {
            Debug.LogError($"Prefab {gameObject.name} null");
        }
        RB = GetComponent<Rigidbody2D>();
        AliveGo=transform.Find("Alive").gameObject;
        facingDirection = 1;

     

        StateMachine = new EnemyStateMachine();

        DieState = new EnemyDieState(this, StateMachine, EnemyData, "die");
    }
    public virtual void Start()
    {
        if (Player.Instance != null)
        {
            _player = Player.Instance.transform;
        }
        
    }

    public virtual void Update()
    {
        if (_player == null) return;
        if (Vector2.Distance(transform.position, _player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity,facingDirection *velocity);
        RB.linearVelocity = velocityWorkspace;

    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        AliveGo.transform.Rotate(0f, 180f, 0f);
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