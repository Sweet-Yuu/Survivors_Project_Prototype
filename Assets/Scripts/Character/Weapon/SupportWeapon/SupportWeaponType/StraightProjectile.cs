using UnityEngine;

public class StraightProjectile : BaseProjectile
{
    private Vector2 moveDirection;
    private float speed;

    public void SetupStraight(Vector2 direction, float weaponDamage, float projectileSpeed, float lifeTime)
    {
        // Kế thừa setup cơ bản
        base.SetupBase(weaponDamage, lifeTime);

        moveDirection = direction.normalized;
        speed = projectileSpeed;

        // Xoay đầu đạn theo hướng bắn
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void Update()
    {
        base.Update(); // Vẫn chạy bộ đếm thời gian sống của Base

        // Bay thẳng theo đường cố định
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            ReturnToPool(); // Trúng đích thì thu hồi
        }
    }
}