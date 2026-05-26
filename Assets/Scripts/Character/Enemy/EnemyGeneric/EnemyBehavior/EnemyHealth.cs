using UnityEngine;

public abstract class EnemyHealth : MonoBehaviour, IDamageable
{
    protected Enemy enemy; 
    protected float currentHealth;

    public Vector2 HitDirection { get; private set; }

    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    protected virtual void Start()
    {
        if (enemy != null && enemy.EnemyData != null)
        {
            currentHealth = enemy.EnemyData.maxHealth;
        }
    }

    
    public virtual void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
       
        

       
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            HitDirection = (transform.position - player.transform.position).normalized;
        }
        else
        {
            HitDirection = Vector2.zero;
        }

    
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            TriggerHurt();
        }
    }

    
    protected abstract void TriggerHurt();
    protected abstract void Die();
}