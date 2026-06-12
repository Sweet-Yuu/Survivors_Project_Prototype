using UnityEngine;

public class AuraProjectile : BaseProjectile
{
    private Transform followTarget; // Vị trí bám theo (Nhân vật)
    private float auraRadius;
    private LayerMask enemyLayer;

    private float tickRate = 0.5f; // Cứ 0.5 giây gây sát thương 1 lần
    private float tickTimer;

    public void SetupAura(Transform playerTransform, float weaponDamage, float lifeTime, float radius, LayerMask layer)
    {
        base.SetupBase(weaponDamage, lifeTime);
        followTarget = playerTransform;
        auraRadius = radius;
        enemyLayer = layer;
        tickTimer = 0f; // Bắt đầu sát thương ngay lập tức

        transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
    }

    protected override void Update()
    {
        base.Update();

        // 1. Luôn đính kèm và di chuyển cùng người chơi
        if (followTarget != null)
        {
            transform.position = followTarget.position;
        }

        // 2. Gây sát thương AOE theo nhịp đếm (Tick)
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            DealAoeDamage();
            tickTimer = tickRate; // Reset nhịp đếm
        }
    }

    private void DealAoeDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, auraRadius, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Truyền sát thương, KHÔNG thu hồi Aura
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    // Vẽ vòng tròn Aura trong Editor để dễ quan sát
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }
}