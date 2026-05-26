using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        [HideInInspector]public int waveQuota;
        public float spawnInterval;
       [HideInInspector] public int spawnCount;
    }
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        [HideInInspector]public int spawnCount;
        public GameObject enemyPrefabs;
    }
    public List<Wave> waves;
    [HideInInspector]public int currentWaveCount;

    [Header("Spawner Attributes")]
    [HideInInspector]float spawnTimer;
    [HideInInspector]public int enemiesAlive;
    public int maxEnemiesAllowed;
    [HideInInspector]public bool maxEnemiesReached=false;
    public float waveInterval;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoint;

    Transform player;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        player = FindFirstObjectByType<Player>().transform;
        CalculateWaveQuota();
       
    }
    private void Update()
    {
        if (GameManager.Instance.score<=5f)
        {
            return;
        }
        if (currentWaveCount<waves.Count && waves[currentWaveCount].spawnCount==0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }
    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        if (currentWaveCount<waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var EnemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += EnemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota= currentWaveQuota;
        
    }
    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var EnemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (EnemyGroup.spawnCount < EnemyGroup.enemyCount)
                {
                    if (enemiesAlive>=maxEnemiesAllowed)
                    {
                        maxEnemiesReached= true;
                        return;
                    }

                    Instantiate(EnemyGroup.enemyPrefabs, player.position + relativeSpawnPoint[Random.Range(0, relativeSpawnPoint.Count)].position, Quaternion.identity);

                    

                    EnemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive<maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
