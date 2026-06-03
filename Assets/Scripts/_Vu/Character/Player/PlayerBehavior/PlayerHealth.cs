using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private Player player;
    private PlayerStats playerStats; 

    public Vector2 HitDirection { get; private set; }
    public float CurrentHealth { get; private set; }
    public GameObject bloodStainPrefab;

    public event Action OnHealthChanged;

    private bool hasSpawnedBloodInThisHit = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>(); 

        CurrentHealth = player.PlayerData.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (player.StateMachine.CurrentState == player.DashState) return;
        if (Player.Instance.isInvincible) return;

        hasSpawnedBloodInThisHit = false;

     
        float finalDamageReceived = damage;

        if (playerStats != null)
        {
            
            float damageReductionPercent = playerStats.def / (playerStats.def + 100f);
            finalDamageReceived = damage * (1f - damageReductionPercent);

           
            finalDamageReceived = Mathf.Max(1f, Mathf.Round(finalDamageReceived * 10f) / 10f);

            Debug.Log($"[PLAYER HEALTH] Enemy deal: {damage} Dmg | Hav {playerStats.def} Def -> Block {finalDamageReceived} Dmg");
        }
        
        CurrentHealth -= finalDamageReceived;

        OnHealthChanged?.Invoke();

        HitDirection = -player.InputHandler.MouseDirection.normalized;

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
            bloodStain.transform.position += Vector3.down * 0.8f;
        }
    }
}