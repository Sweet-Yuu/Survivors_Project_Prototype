using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    private Rigidbody2D rb;
    private Bow bow; // Reference to the enemy that shot the arrow

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
            bow = GetComponentInParent<Bow>(); // Assuming the arrow is a child of the enemy that shot it

    }
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Enemy enemy = collision.GetComponent<Enemy>();

        
        if (collision.CompareTag("Enemy"))
        {
            
            enemy.TakeDamage(bow.BowData.attackDamage);
            Destroy(gameObject);
        }
      
        else if (collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
