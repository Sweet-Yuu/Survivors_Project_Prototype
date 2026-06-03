using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon Data/Base Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName;
    public float atk = 10f;
    public float atkSpd = 5f;
    public float crit = 5f;
    public float critDmg = 10f;
}