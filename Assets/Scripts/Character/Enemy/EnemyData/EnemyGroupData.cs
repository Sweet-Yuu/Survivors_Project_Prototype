using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemyGroup", menuName = "Survival/EnemyGroup")]
public class EnemyGroupData : ScriptableObject
{
   public string enemyName;
    public int enemyCount;
    public int spawnCount;
    public GameObject enemyPrefab;
}
