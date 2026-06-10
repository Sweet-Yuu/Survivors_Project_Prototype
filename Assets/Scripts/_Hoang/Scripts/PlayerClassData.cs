using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "Moonblight/Player Class")]
public class PlayerClassData : ScriptableObject
{
    public string className;
    public int maxBaseHealth;
    public float maxMoveSpeed;
    public float baseDamageMultiplier;
}
