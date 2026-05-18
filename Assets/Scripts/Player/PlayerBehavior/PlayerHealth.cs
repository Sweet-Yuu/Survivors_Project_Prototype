using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    private Player player;
    public Vector2 HitDirection { get; private set; }   

    public int CurrentHealth { get; private set; }
    public GameObject bloodStainPrefab;

    [Header("Invincibility Settings")]
    [SerializeField] private float invincibilityDuration = 0.4f;
    private float lastImmortalTime = -999f;

    private bool hasSpawnedBloodInThisHit = false;

    public bool IsInvincible => Time.time < lastImmortalTime + invincibilityDuration;

    private void Awake()
    {
        player = GetComponent<Player>();

        CurrentHealth = player.PlayerData.maxHealth;
    }


    public void TakeDamage(float damage)
    {
        if (player.StateMachine.CurrentState == player.DashState)
        {
            Debug.Log("Dodge");
            return;
        }
        if (IsInvincible)
        {
            Debug.Log("Immortal");
            return;
        }
        lastImmortalTime = Time.time;

        hasSpawnedBloodInThisHit = false;

        int damageInt = Mathf.RoundToInt(damage);
        CurrentHealth -= damageInt;

        Debug.Log("Current HP: " + CurrentHealth);

        HitDirection = -player.InputHandler.MouseDirection.normalized;
        //if (bloodStainPrefab != null) { SpawnBloodStain(); }
        if (CurrentHealth > 0)
        {
            player.StateMachine.ChangeState(player.TakeDamageState);
        }
        else
        {
            
            player.StateMachine.ChangeState(player.DieState);
        }

    }
    public void SpawnBloodStain()
    {
        if (hasSpawnedBloodInThisHit) return;
        if (bloodStainPrefab != null)
        {
            hasSpawnedBloodInThisHit = true;

            GameObject bloodStain = Instantiate(bloodStainPrefab, transform.position, Quaternion.identity);
            float randomRotation = Random.Range(0f, 360f);
            bloodStain.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
    }
}