using UnityEngine;

public class BowAttack
{
    private Bow bow;
    private GameObject arrowPrefab;
    private Transform firePoint;

    // Biến để quản lý thời gian hồi chiêu thực tế
    private float nextAttackTime;
    private float baseCooldown = 1f; // Thời gian giãn cách gốc khi spd = 1 (1 giây bắn 1 phát)

    // Thuộc tính để BowIdleState kiểm tra xem đã được bắn chưa
    public bool CanAttack => Time.time >= nextAttackTime;

    public BowAttack(Bow bow, GameObject arrowPrefab, Transform firePoint)
    {
        this.bow = bow;
        this.arrowPrefab = arrowPrefab;
        this.firePoint = firePoint;
    }

    public void StartAttack(float damage)
    {
        if (arrowPrefab == null || firePoint == null) return;

        // 1. Sinh ra mũi tên
        GameObject arrowGo = Object.Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Arrow arrowScript = arrowGo.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            arrowScript.SetupArrowData(damage);
        }

        // 2. TÍNH TOÁN THỜI GIAN COOLDOWN ĐỘNG THEO PLAYER STATS
        float playerAttackSpd = 1f;
        if (Player.Instance != null)
        {
            PlayerStats stats = Player.Instance.GetComponent<PlayerStats>();
            if (stats != null)
            {
                playerAttackSpd = stats.spd; // Lấy spd từ PlayerStats
            }
        }

        // Tốc độ đánh càng cao (spd > 1) thì khoảng thời gian chờ càng ngắn lại
        float actualCooldown = baseCooldown / playerAttackSpd;
        nextAttackTime = Time.time + actualCooldown;
    }

    public void EndAttack()
    {
        // Để trống hoặc giữ nguyên code cũ của bạn
    }

    public void ResetCooldown()
    {
        // Nếu trước đó bạn dùng hàm này để tính cooldown, hãy xóa logic cũ đi để tránh xung đột
    }
}