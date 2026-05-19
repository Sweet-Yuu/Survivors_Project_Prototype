using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
