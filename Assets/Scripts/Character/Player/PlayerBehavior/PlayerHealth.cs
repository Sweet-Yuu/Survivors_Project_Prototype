using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    private Player player;
    public Vector2 HitDirection { get; private set; }
    public float CurrentHealth { get; private set; }
    public GameObject bloodStainPrefab;

    

    private bool hasSpawnedBloodInThisHit = false;

    

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
        if (Player.Instance.isInvincible)
        {
            return;
        }
        

        hasSpawnedBloodInThisHit = false;

        
        CurrentHealth -= damage;

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