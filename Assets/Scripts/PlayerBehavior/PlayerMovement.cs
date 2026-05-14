using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerData playerData;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerData = GetComponent<Player>().PlayerData;
    }

    public void Move(Vector2 input)
    {
        rb.linearVelocity = input.normalized * playerData.moveSpeed;
    }

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }
}