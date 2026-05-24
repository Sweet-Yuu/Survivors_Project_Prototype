using UnityEngine;

public class EnemyState 
{
    public Enemy enemy;
    public EnemyStateMachine stateMachine;
    public EnemyData enemyData;

    protected float startTime;

    private string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        DoChecks();
        enemy.Visual.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        
    }
    public virtual void Exit()
    {
        enemy.Visual.Anim.SetBool(animBoolName, false);
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
