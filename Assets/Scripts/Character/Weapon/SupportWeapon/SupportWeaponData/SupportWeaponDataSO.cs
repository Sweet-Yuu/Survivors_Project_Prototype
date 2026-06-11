using UnityEngine;

[CreateAssetMenu(fileName = "NewSupportWeapon", menuName = "Game Data/Weapons/Support Weapon")]
public class SupportWeaponDataSO : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName = "Fireball";
    public float damage = 10f;
    public float fireRate = 1f;
    public float attackRange = 8f;
    public float projectileSpeed = 10f;

    public int projectileCount = 1;

    [Header("Visuals")]
    public GameObject projectilePrefab;
}