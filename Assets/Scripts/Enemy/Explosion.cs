using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int damage = 25;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 1. Nếu vùng nổ chạm vào Player
        if (collider.CompareTag("Player"))
        {
            PlayerHealth health = collider.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        // 2. Nếu vùng nổ chạm vào Quái vật khác
        else if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}