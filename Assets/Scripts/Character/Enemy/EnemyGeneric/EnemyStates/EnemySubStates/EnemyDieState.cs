using UnityEngine;

public class EnemyDieState : EnemyState
{
    private SpriteRenderer spriteRenderer;
    private float dieStartTime;

    private float fadeDelay = 2f;
    private float fadeDuration = 4f;
    private bool hasSpawnedExp = false;

    public EnemyDieState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName)
        : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.RB.linearVelocity = Vector2.zero;
        dieStartTime = Time.time;
        hasSpawnedExp = false;

        
        Collider2D collider = enemy.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        
        if (enemy.AliveGo != null)
        {
            spriteRenderer = enemy.AliveGo.GetComponentInChildren<SpriteRenderer>();
        }

       
        if (enemy.anim != null)
        {
            enemy.anim.SetTrigger("die");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float timePassed = Time.time - dieStartTime;

       
        if (timePassed >= fadeDelay)
        {
            if (spriteRenderer != null)
            {
                float pct = (timePassed - fadeDelay) / fadeDuration;
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1f, 0f, pct);
                spriteRenderer.color = color;
            }

            
            if (timePassed >= fadeDelay + fadeDuration)
            {
                
                Object.Destroy(enemy.gameObject);
            }
            SpawnExperience();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void SpawnExperience()
    {
        if (hasSpawnedExp) return;
        hasSpawnedExp = true;

        if (enemy.EnemyData != null && enemy.EnemyData.experiencePrefab != null)
        {
            Object.Instantiate(enemy.EnemyData.experiencePrefab, enemy.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("null expOrb");
        }
    }
}