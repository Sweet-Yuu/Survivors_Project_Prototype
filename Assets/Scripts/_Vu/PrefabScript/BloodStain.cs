using UnityEngine;

public class BloodStain : MonoBehaviour
{
    public float fadeSpeed = 0.5f; // Time it takes for the blood stain to fade out
    public float delayBeforeFade = 1f; 
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float spawnTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnTime = Time.time;
        originalColor = spriteRenderer.color;
    }
    private void Update()
    {
        if (Time.time >= spawnTime + delayBeforeFade)
        {
            originalColor.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = originalColor;
            if (originalColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
