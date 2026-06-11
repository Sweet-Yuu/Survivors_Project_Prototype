using UnityEngine;
using UnityEngine.Pool;

public class SupportWeaponProjectile : MonoBehaviour
{
    private IObjectPool<GameObject> pool;
    private float damage;
    private float speed;
    private Vector2 moveDirection;

    // Đồng hồ đếm thời gian sống
    private float lifeTimer;

    // Gắn thẻ Pool cho đạn
    public void SetPool(IObjectPool<GameObject> poolReference)
    {
        pool = poolReference;
    }

    public void Setup(Vector2 direction, float weaponDamage, float projectileSpeed)
    {
        moveDirection = direction.normalized;
        damage = weaponDamage;
        speed = projectileSpeed;

        // Reset thời gian sống mỗi lần bắn (5 giây)
        lifeTimer = 5f;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        // Bay thẳng theo hướng
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        // Tự giảm thời gian sống, nếu hết thì quay về kho (thay thế cho Destroy)
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            ReturnToPool(); // Thu hồi viên đạn ngay khi trúng mục tiêu
        }
    }

    private void ReturnToPool()
    {
        // Kiểm tra an toàn: Chỉ thu hồi nếu đạn đang được bật
        if (gameObject.activeInHierarchy)
        {
            if (pool != null)
            {
                pool.Release(gameObject);
            }
            else
            {
                // Dùng dự phòng nếu ai đó quên gắn Pool
                Destroy(gameObject);
            }
        }
    }
}