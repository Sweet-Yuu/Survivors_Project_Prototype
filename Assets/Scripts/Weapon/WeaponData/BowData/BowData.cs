using UnityEngine;
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon Data/Bow Data")]
public class BowData : ScriptableObject
{
    [Header("Attack Data")]
    public float attackSpeed = 1f;
    public float attackDamage = 15f;
    [Header("Critical Data %")]
    public float attackCrit = 5f;
    public float attackCritDmg = 100f;

}
