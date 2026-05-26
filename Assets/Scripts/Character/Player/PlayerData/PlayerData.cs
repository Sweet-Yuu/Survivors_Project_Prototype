using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health Data")]
    public float maxHealth = 100f;
    [Header("Dmg Data")]
    public float damage = 1f;
    public float attackSpd = 1f;
    public float critChance = 10f;
    public float critDmg = 10f;
    [Header("Def Data")]
    public float maxDef = 10f;
    [Header("Movement Data")]
    public float moveSpeed = 5f;

    [Header("Dash Data")]
    public float dashDuration = 0.7f;
    public float dashSpeed = 10f;
    public float dashCooldown = 1f;

    

}
