using UnityEngine;


public abstract class EnemyData : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float maxHealth=100f;
    public float moveSpeed=5f;
    public float attackRange=5f;
    public float damage=10f;
    
    public GameObject experiencePrefab;
}
