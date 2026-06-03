using UnityEngine;

public class Player : MonoBehaviour
{
   
    public PlayerHealth Health { get; private set; }
    public PlayerDash Dash { get; private set; }
    public PlayerExperience Experience { get; private set; }
   


    public PlayerStateMachine StateMachine { get; private set; }



    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerTakedamageState TakeDamageState { get; private set; }
    public PlayerDieState DieState { get; private set; }
    public PlayerDashState DashState { get; private set; }


    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Vector2 LastInput { get; set; }
    public bool isInvincible { get; set; }
    public static bool IsDead = false;

    public static Player Instance { get; private set; }

    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData => playerData;

    public PlayerStats playerStats {  get; private set; }
    

    private void Awake()
    {
        Instance = this;
        Anim = GetComponentInChildren<Animator>();
        playerStats = GetComponent<PlayerStats>();

        InputHandler = GetComponent<PlayerInputHandler>();

        RB = GetComponent<Rigidbody2D>();

     
        Health = GetComponent<PlayerHealth>();
        Dash = GetComponent<PlayerDash>();
        Experience = GetComponent<PlayerExperience>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        TakeDamageState = new PlayerTakedamageState(this, StateMachine, playerData, "hurt");
        DieState = new PlayerDieState(this, StateMachine, playerData, "die");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");

        
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
    public void TriggerBloodSpawnEvent()
    {
        if (Health != null)
        {
            Health.SpawnBloodStain();
        }
    }
    public void IsInvincible(bool value)
    {
        isInvincible = value;
    }
    
    public float CalculateOutputDamage()
    {

        if (playerStats == null)
        {
            Debug.LogWarning("PlayerStats null");
            return PlayerData != null ? PlayerData.damage : 10f;
        }

        
        float baseDamage = playerStats.dmg;

        
        float roll = Random.Range(0f, 100f);
        if (roll <= playerStats.crit)
        {
            
            float criticalDamage = baseDamage * (1f + (playerStats.critDmg / 100f));

            Debug.Log($"<color=red>[CRITICAL HIT!]</color> Base: {baseDamage} -> Crit: {criticalDamage}");
            return criticalDamage;
        }

        return baseDamage;
    }
}