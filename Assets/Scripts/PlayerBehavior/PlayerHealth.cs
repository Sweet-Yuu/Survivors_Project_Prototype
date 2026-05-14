using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Player player;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();

        CurrentHealth = player.PlayerData.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        Debug.Log("Current HP: " + CurrentHealth);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            player.StateMachine.ChangeState(player.DieState);
        }
        else
        {
            player.StateMachine.ChangeState(player.TakeDamageState);
        }
    }
}