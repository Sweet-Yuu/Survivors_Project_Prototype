using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health Data")]
    public float maxHealth = 100f;
    [Header("Movement Data")]
    public float moveSpeed = 5f;
    [Header("Def Data")]
    public float maxDef = 10f;
    [Header("Dash Data")]
    public float dashDuration = 0.7f;
    public float dashSpeed = 10f;
    public float dashCooldown = 1f;



}
