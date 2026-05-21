using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    private Rigidbody2D rb;
    private Bow bow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bow = GetComponentInParent<Bow>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            
            EnemyHealth enemyHealth = collision.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                float damage = (bow != null && bow.BowData != null) ? bow.BowData.attackDamage : 10f;

               
                enemyHealth.TakeDamage(damage, transform.position);
            }

            Destroy(gameObject);
        }
    }
}