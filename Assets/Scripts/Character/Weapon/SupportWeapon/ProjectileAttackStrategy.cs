using UnityEngine;
using UnityEngine.Pool;

public class ProjectileAttackStrategy : MonoBehaviour, IWeaponAttackStrategy
{
    [SerializeField] private float spreadAngle = 20f;
    [SerializeField] private LayerMask enemyLayer; // Cần thêm Layer để Homing/Aura biết quét quái

    private ObjectPool<GameObject> projectilePool;
    private GameObject prefabToPool;

    public void ExecuteAttack(Transform firePoint, Transform target, SupportWeaponDataSO weaponData)
    {
        if (weaponData.projectilePrefab == null || firePoint == null) return;

        // Khởi tạo Pool
        if (projectilePool == null)
        {
            prefabToPool = weaponData.projectilePrefab;
            projectilePool = new ObjectPool<GameObject>(
                createFunc: CreateNewProjectile,
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                defaultCapacity: 20,
                maxSize: 100
            );
        }

        Vector2 baseDirection = target != null ? (target.position - firePoint.position).normalized : Vector2.right;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - (spreadAngle * (weaponData.projectileCount - 1) / 2f);

        for (int i = 0; i < weaponData.projectileCount; i++)
        {
            GameObject projectileObj = projectilePool.Get();
            projectileObj.transform.position = firePoint.position;

            float currentAngle = startAngle + (spreadAngle * i);
            Vector2 fireDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            // TÍNH ĐA HÌNH: Tự động nhận diện loại đạn và gọi hàm Setup tương ứng
            if (projectileObj.TryGetComponent(out StraightProjectile straight))
            {
                straight.SetupStraight(fireDirection, weaponData.damage, weaponData.projectileSpeed, 5f);
            }
            else if (projectileObj.TryGetComponent(out HomingProjectile homing))
            {
                homing.SetupHoming(target, weaponData.damage, weaponData.projectileSpeed, 5f, enemyLayer);
            }
            else if (projectileObj.TryGetComponent(out AuraProjectile aura))
            {
                // Vòng Aura thì gắn trực tiếp vào vị trí Player (firePoint)
                aura.SetupAura(firePoint, weaponData.damage, 5f, weaponData.attackRange, enemyLayer);
            }
        }
    }

    private GameObject CreateNewProjectile()
    {
        GameObject obj = Instantiate(prefabToPool);

        // Lấy class cha BaseProjectile để gán Pool (Áp dụng cho mọi loại đạn)
        BaseProjectile script = obj.GetComponent<BaseProjectile>();
        if (script != null)
        {
            script.SetPool(projectilePool);
        }
        return obj;
    }
}