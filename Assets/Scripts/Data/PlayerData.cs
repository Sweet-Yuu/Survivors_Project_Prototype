using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health Data")]
    public int maxHealth = 100;
    [Header("Movement Data")]
    public float moveSpeed = 5f;

}
