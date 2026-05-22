using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<WaveData> waves;
    public int currenrWaveCount;

    Transform player;

    private void Start()
    {
        player = FindFirstObjectByType<Player>().transform;
        CalculateWaveQuota();
        SpawnEnemies();
    }
    private void Update()
    {
        
    }
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var EnemyGroupData in waves[currenrWaveCount].enemyGroups)
        {
            currentWaveQuota += EnemyGroupData.enemyCount;
        }
        waves[currenrWaveCount].waveQuota= currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }
    void SpawnEnemies()
    {
        if (waves[currenrWaveCount].spawnCount < waves[currenrWaveCount].waveQuota)
        {
            foreach (var EnemyGroupData in waves[currenrWaveCount].enemyGroups)
            {
                if (EnemyGroupData.spawnCount < EnemyGroupData.enemyCount)
                {
                    Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10f,10f),player.transform.position.y + Random.Range(-10f, 10f));
                    Instantiate(EnemyGroupData.enemyPrefab,spawnPosition,Quaternion.identity);
                    EnemyGroupData.spawnCount++;
                    waves[currenrWaveCount].spawnCount++;
                }
            }
        }
    }
}
