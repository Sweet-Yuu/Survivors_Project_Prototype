using UnityEngine;

public class BasicEnemy : Enemy
{
    public PlayerHealth playerHealth;
    public void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
            }
        }
    }
}
