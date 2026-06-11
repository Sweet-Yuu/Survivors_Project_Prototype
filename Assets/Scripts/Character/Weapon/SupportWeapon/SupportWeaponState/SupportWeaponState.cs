using UnityEngine;

public abstract class SupportWeaponState
{
    protected SupportWeaponController controller;
    protected SupportWeaponStateMachine stateMachine;
    protected SupportWeaponDataSO weaponData;

    protected float startTime;

    public SupportWeaponState(SupportWeaponController controller, SupportWeaponStateMachine stateMachine, SupportWeaponDataSO weaponData)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
        this.weaponData = weaponData;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }
}