using UnityEngine;
using UnityEngine.Pool; // Bắt buộc phải có để dùng Object Pool

public class ProjectileAttackStrategy : MonoBehaviour, IWeaponAttackStrategy
{
    [SerializeField] private float spreadAngle = 20f;

    // Khai báo Pool và Prefab gốc
    private ObjectPool<GameObject> projectilePool;
    private GameObject prefabToPool;

    public void ExecuteAttack(Transform firePoint, Transform target, SupportWeaponDataSO weaponData)
    {
        if (weaponData.projectilePrefab == null || firePoint == null || target == null) return;

        // Khởi tạo Pool một lần duy nhất vào lần bắn đầu tiên
        if (projectilePool == null)
        {
            prefabToPool = weaponData.projectilePrefab;
            projectilePool = new ObjectPool<GameObject>(
                createFunc: CreateNewProjectile,
                actionOnGet: (obj) => obj.SetActive(true),   // Bật lên khi rút ra khỏi kho
                actionOnRelease: (obj) => obj.SetActive(false),// Tắt đi khi trả về kho
                actionOnDestroy: (obj) => Destroy(obj),
                defaultCapacity: 20, // Chuẩn bị sẵn 20 viên lúc đầu
                maxSize: 100 // Tối đa chứa 100 viên
            );
        }

        // Tính toán góc bắn hình quạt
        Vector2 baseDirection = (target.position - firePoint.position).normalized;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - (spreadAngle * (weaponData.projectileCount - 1) / 2f);

        // Xả đạn
        for (int i = 0; i < weaponData.projectileCount; i++)
        {
            // LẤY ĐẠN TỪ POOL THAY VÌ INSTANTIATE
            GameObject projectileObj = projectilePool.Get();

            // Đặt lại vị trí nòng súng
            projectileObj.transform.position = firePoint.position;

            // Tính góc và hướng cho viên đạn
            float currentAngle = startAngle + (spreadAngle * i);
            Vector2 fireDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            SupportWeaponProjectile projectileScript = projectileObj.GetComponent<SupportWeaponProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Setup(fireDirection, weaponData.damage, weaponData.projectileSpeed);
            }
        }
    }

    // Hàm quy định cách Pool tạo ra viên đạn mới khi kho trống
    private GameObject CreateNewProjectile()
    {
        GameObject obj = Instantiate(prefabToPool);

        SupportWeaponProjectile script = obj.GetComponent<SupportWeaponProjectile>();
        if (script != null)
        {
            // Cấp cho viên đạn cái thẻ từ để nó biết đường quay về kho
            script.SetPool(projectilePool);
        }
        return obj;
    }
}