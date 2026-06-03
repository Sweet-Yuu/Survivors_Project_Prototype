using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 10f;
    public float damageRate = 1f; // Tốc độ trừ máu (ví dụ: 1 giây trừ 1 lần nếu cứ đứng dính vào quái)

    private float nextDamageTime;

    // Dùng Stay thay vì Enter để nếu Player đứng yên sát vào bot, bot vẫn tiếp tục gây sát thương
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    private void DealDamage()
    {
        // Kiểm tra xem đã đến thời gian được phép gây sát thương tiếp theo chưa
        if (Time.time >= nextDamageTime)
        {
            // Có thể dùng Player.Instance như cũ của bạn
            if (Player.Instance != null && Player.Instance.Health != null)
            {
                // Gọi hàm TakeDamage bên script PlayerHealth
                Player.Instance.Health.TakeDamage(damageAmount);

                // Đặt lại thời gian đếm ngược cho lần cắn tiếp theo
                nextDamageTime = Time.time + damageRate;
            }
        }
    }
}