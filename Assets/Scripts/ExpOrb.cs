using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    [SerializeField] private int expAmount = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();

            if (player != null)
            {
                player.AddExp(expAmount);
            }

            if (ResourceManager.Instance != null)
            {
                ResourceManager.Instance.AddExp(expAmount);
            }
            Destroy(gameObject);
        }
    }
}