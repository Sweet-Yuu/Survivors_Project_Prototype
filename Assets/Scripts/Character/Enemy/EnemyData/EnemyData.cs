using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyrData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float maxHealth=100f;
    public float moveSpeed=5f;
    public float attackRange=5f;
    public float damage=10f;

    [Header("Enemy States")]
    public float stunDuration=0.5f;
}
