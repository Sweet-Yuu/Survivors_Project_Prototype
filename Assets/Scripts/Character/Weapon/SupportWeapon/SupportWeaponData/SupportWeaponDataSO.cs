using UnityEngine;

//public enum WeaponRarity
//{
//    Common,
//    Rare,
//    Epic,
//    Legendary
//}

[CreateAssetMenu(fileName = "NewSupportWeapon", menuName = "Game Data/Weapons/Support Weapon")]
public class SupportWeaponDataSO : ScriptableObject
{
    //[Header("General Information")]
    //public string weaponName = "New Weapon";
    //public Sprite weaponIcon; // Dùng cho UI thẻ bài khi Level Up
    //public WeaponRarity rarity = WeaponRarity.Common;

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