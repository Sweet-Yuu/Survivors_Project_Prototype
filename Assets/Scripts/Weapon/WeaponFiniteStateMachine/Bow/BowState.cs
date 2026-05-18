using UnityEngine;

public class BowState 
{
    protected Bow bow;
    protected BowStateMachine stateMachine;
    protected BowData bowData;

    protected float startTime;

    public string animBoolName;

    public BowState(Bow bow, BowStateMachine stateMachine, BowData bowData, string animBoolName)
    {
        this.bow = bow;
        this.stateMachine = stateMachine;
        this.bowData = bowData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        bow.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
    }
    public virtual void Exit()
    {
        bow.Anim.SetBool(animBoolName, false);
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
