using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    [SerializeField] private int expAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();

            if (player != null)
            {
                player.AddExp(expAmount);
            }

            Destroy(gameObject);
        }
    }
}