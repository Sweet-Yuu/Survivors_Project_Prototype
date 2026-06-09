using UnityEngine;

public interface IWeaponAttackStrategy 
{
    void ExecuteAttack(Transform firePoint, Transform target, SupportWeaponDataSO weaponData);
}
