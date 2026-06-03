using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float speed = 15f; 

    private Rigidbody2D rb;
    private float finalDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
      
        if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }

        Destroy(gameObject, lifetime);
    }

    public void SetupArrowData(float damage)
    {
        finalDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(finalDamage);
            }
            Destroy(gameObject);
        }
    }
}