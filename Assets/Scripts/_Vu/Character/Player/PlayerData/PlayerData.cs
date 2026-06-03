using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Stat Data")]
    public float maxHealth ; 
    public float damage ;
    public float attackSpd ;
    public float critChance ;
    public float critDmg;
    public float maxDef;
    public float moveSpeed;

    [Header("Dash Data")]
    public float dashDuration;
    public float dashSpeed;
    public float dashCooldown;

    

}
