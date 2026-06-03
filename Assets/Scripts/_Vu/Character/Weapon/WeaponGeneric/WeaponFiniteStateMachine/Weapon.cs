using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponIdleState IdleState { get; private set; }
    public WeaponStateMachine StateMachine { get; private set; }

    public WeaponVisual Visual { get; private set; }

    public static Weapon Instance { get; private set; }

    [SerializeField] private WeaponData weaponData;
    public WeaponData WeaponData => weaponData;


    private void Awake()
    {
        Instance = this;
        Visual=GetComponentInChildren<WeaponVisual>();
        
        StateMachine = new WeaponStateMachine();

        IdleState= new WeaponIdleState(this, StateMachine, WeaponData, "idle");
    }

    protected virtual void Start()
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
}
