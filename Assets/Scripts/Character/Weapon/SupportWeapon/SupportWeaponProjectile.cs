using UnityEngine;

public class SupportWeaponProjectile : MonoBehaviour
{
    private Transform target;
    private float damage;
    private float speed;
    private Vector2 moveDirection;

    public void Setup(Transform enemyTarget, float weaponDamage, float projectileSpeed)
    {
        target = enemyTarget;
        damage = weaponDamage;
        speed = projectileSpeed;

        if (target != null)
        {
            moveDirection = (target.position - transform.position).normalized;
            RotateTowardsDirection();
        }

        // Hủy đạn sau 5 giây để tránh kẹt bộ nhớ
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (target != null)
        {
            moveDirection = (target.position - transform.position).normalized;
            RotateTowardsDirection();
        }

        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void RotateTowardsDirection()
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cơ chế đa hình lấy SkeletonHealth, ZombieHealth thông qua lớp cha EnemyHealth
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            // Trừ máu theo đúng hàm có sẵn trong hệ thống gốc
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}