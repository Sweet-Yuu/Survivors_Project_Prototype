using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    [Header("ExpOrb")]
    public int expValue = 10;
    public float moveSpeed = 8f;

    private bool isCollected = false;
    private bool isConsumed = false;
    private Transform playerTransform;

    private void Update()
    {
        if (isCollected && playerTransform != null && !isConsumed)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            playerTransform = other.transform;
            isCollected = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isCollected && !isConsumed && other.CompareTag("Player"))
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance < 0.5f)
            {
                isConsumed = true;

                PlayerExperience playerLvl = other.GetComponent<PlayerExperience>();

                if (playerLvl != null)
                {
                    playerLvl.GainExperience(expValue);
                    
                }
                Destroy(gameObject);
            }
        }
    }

}