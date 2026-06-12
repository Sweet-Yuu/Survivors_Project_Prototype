using UnityEngine;

public class HomingProjectile : BaseProjectile
{
    private Transform currentTarget;
    private float speed;
    private float turnSpeed = 300f; // Tốc độ bẻ lái
    private float searchRadius = 15f; // Tầm quét tìm mục tiêu thay thế
    private LayerMask enemyLayer;

    public void SetupHoming(Transform target, float weaponDamage, float projectileSpeed, float lifeTime, LayerMask layer)
    {
        base.SetupBase(weaponDamage, lifeTime);
        currentTarget = target;
        speed = projectileSpeed;
        enemyLayer = layer;
    }

    protected override void Update()
    {
        base.Update();

        // 1. Quét tìm mục tiêu mới nếu quái cũ đã chết hoặc lọt khỏi tầm nhìn
        if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            FindNewTarget();
        }

        // 2. Thuật toán Steering (Bẻ lái rượt đuổi)
        if (currentTarget != null)
        {
            Vector2 direction = (currentTarget.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Xoay từ từ về phía mục tiêu thay vì giật cục
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // 3. Tiến về phía trước theo hướng mũi đạn đang chĩa vào
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void FindNewTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, enemyLayer);
        float closestDist = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider2D hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = hit.transform;
            }
        }
        currentTarget = closestEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            ReturnToPool();
        }
    }
}