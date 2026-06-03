using UnityEngine;

public class WeaponState
{
    protected Weapon weapon;
    protected WeaponStateMachine stateMachine;
    protected WeaponData weaponData;

    protected float startTime;

    public string animBoolName;

    public WeaponState(Weapon weapon,WeaponStateMachine stateMachine,WeaponData weaponData,string animBoolName)
    {
       this.weapon = weapon;
        this.stateMachine = stateMachine;
        this.weaponData = weaponData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        weapon.Visual.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
    }
    public virtual void Exit()
    {
        weapon.Visual.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }


    public virtual void DoChecks()
    {

    }

}
