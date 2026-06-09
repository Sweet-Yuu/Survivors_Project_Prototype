using UnityEngine;

public class SupportWeaponController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Weapon Configuration")]
    [SerializeField] private SupportWeaponDataSO weaponData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemyLayer; //Đây là layer của kẻ địch để kiểm tra va chạmx

    private IWeaponAttackStrategy attackStrategy;
    private float FireTimer; // Biến đếm thời gian giữa các lần bắn

    private void Awake()
    {
        attackStrategy =GetComponent<IWeaponAttackStrategy>();
    }    

    private void Update()
    {
       if(weaponData == null || attackStrategy == null) return;

        FireTimer -= Time.deltaTime;
        
        if(FireTimer <= 0f)
        {
            Transform target = FindNearestEnemy();
            if(target != null)
            {
                attackStrategy.ExecuteAttack(firePoint, target,weaponData);
                FireTimer = weaponData.fireRate; // Reset timer sau khi bắn
            }
        }
    }

    private Transform FindNearestEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponData.attackRange, enemyLayer);

        Transform nearestEnemy = null;
        float shortestDistance =Mathf.Infinity;
         foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                float distance =Vector2.Distance(transform.position, enemyCollider.transform.position);
                if(distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = enemyCollider.transform;
                }
            }
        }
         return nearestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        if (weaponData != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, weaponData.attackRange);
        }
    }
}
