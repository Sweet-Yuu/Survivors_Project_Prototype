using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon Data/Bow Data")]
public class BowData : ScriptableObject
{
    [Header("Attack Data")]
    public float attackSpeed;
    public float attackDamage;
    [Header("Critical Data %")]
    public float attackCrit;
    public float attackCritDmg;

}
