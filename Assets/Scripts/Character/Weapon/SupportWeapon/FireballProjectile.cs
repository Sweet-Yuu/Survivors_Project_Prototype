using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;
    private float damage;

    // Nạp dư liệu cho viên đạn
    public void Setup(Vector3 direction, float speed, float damage)
    {
        this.moveDirection = direction.normalized;
        this.speed = speed;
        this.damage = damage;

        // Xoay viên đạn theo hướng di chuyển
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Hủy viên đạn sau 5 giây nếu không va chạm
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        // Di chuyển viên đạn
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // Kiểm tra va chạm với kẻ địch
//        if (collision.CompareTag("Enemy"))
//}
