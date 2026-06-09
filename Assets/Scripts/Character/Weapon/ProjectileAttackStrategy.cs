using UnityEngine;

public class ProjectileAttackStrategy : MonoBehaviour, IWeaponAttackStrategy
{
    public void ExecuteAttack(Transform firePoint, Transform target, SupportWeaponDataSO weaponData)
    {
        if (weaponData.projectilePrefab == null || firePoint == null) return;

        // Sinh ra Prefab viên đạn
        GameObject projectileObj = Instantiate(weaponData.projectilePrefab, firePoint.position, Quaternion.identity);

        // Truyền thông số cho viên đạn bay
        SupportWeaponProjectile projectileScript = projectileObj.GetComponent<SupportWeaponProjectile>();
        if (projectileScript != null)
        {
            projectileScript.Setup(target, weaponData.damage, weaponData.projectileSpeed);
        }
    }
}