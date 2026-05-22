
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "Survival/Wave")]
public class WaveData : ScriptableObject
{
    public string waveName;
    public List<EnemyGroupData> enemyGroups;
    public int waveQuota;
    public float spawnInterval;
    public int spawnCount;
}