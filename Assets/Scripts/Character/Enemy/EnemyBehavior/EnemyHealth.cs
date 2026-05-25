using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    private Enemy enemy;
    public Vector2 HitDirection { get; private set; }
    public float CurrentHealth { get; private set; }

    

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        CurrentHealth = enemy.EnemyData.maxHealth;
    }
    public void TakeDamage(float damage)
    {
        Vector2 attackerPos = Player.Instance != null ? (Vector2)Player.Instance.transform.position : (Vector2)transform.position;
        TakeDamage(damage, attackerPos);
    }
    public void TakeDamage(float damage, Vector2 attackerPosition)
    {
        

       
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, enemy.EnemyData.maxHealth);

        Debug.Log("Current HP: " + CurrentHealth);

        
        HitDirection = ((Vector2)transform.position - attackerPosition).normalized;

        if (CurrentHealth > 0)
        {
            
            if (enemy.StateMachine.CurrentState == enemy.HurtState)
            {
                enemy.HurtState.ForceRestartHurt();
            }
            else
            {
                enemy.StateMachine.ChangeState(enemy.HurtState);
            }
        }
        else
        {
            enemy.StateMachine.ChangeState(enemy.DieState);
        }
    }
}
