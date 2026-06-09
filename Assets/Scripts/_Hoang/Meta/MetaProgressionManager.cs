using UnityEngine;
using System;

public class MetaProgressionManager : MonoBehaviour
{
    public static MetaProgressionManager Instance { get; private set; }
    [Header("Currency")]
    public int totalDataPoints;

    [Header("Upgrade Levels")]
    public int maxHpLevel;
    public int dmgLevel;
    public int atkSpdLevel;
    public int critChanceLevel;
    public int critDmgLevel;
    public int defLevel;
    public int moveSpdLevel;

    [Header("Upgrade Scaling")]
    public float hpBonusPerlevel = 10f;
    public float dmgBonusPerLevel = 2f;
    public float defBonusPerLevel = 1.5f;
    public int baseUpgradeCost = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDataPoints(int amount)
    {
        totalDataPoints += amount;
    }

    public bool TryBuyUpgrade(StatType statType)
    {
        int cost = baseUpgradeCost;
        if (totalDataPoints >= cost)
        {
            totalDataPoints -= cost;
            ApplyUpgradeLevel(statType);
            SaveData();
            return true;
        }
        return false;
    }

    private void ApplyUpgradeLevel(StatType statType)
    {
        switch (statType)
        {
            case StatType.MaxHP:
                maxHpLevel++;
                break;
            case StatType.Dmg:
                dmgLevel++;
                break;
            case StatType.AttackSpd:
                atkSpdLevel++;
                break;
            case StatType.CritChance:
                critChanceLevel++;
                break;
            case StatType.CritDmg:
                critDmgLevel++;
                break;
            case StatType.Def:
                defLevel++;
                break;
            case StatType.MoveSpd:
                moveSpdLevel++;
                break;
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("TotalDataPoints", totalDataPoints);
        PlayerPrefs.SetInt("MaxHpLevel", maxHpLevel);
        PlayerPrefs.SetInt("DmgLevel", dmgLevel);
        PlayerPrefs.SetInt("AtkSpdLevel", atkSpdLevel);
        PlayerPrefs.SetInt("CritChanceLevel", critChanceLevel);
        PlayerPrefs.SetInt("CritDmgLevel", critDmgLevel);
        PlayerPrefs.SetInt("DefLevel", defLevel);
        PlayerPrefs.SetInt("MoveSpdLevel", moveSpdLevel);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        totalDataPoints = PlayerPrefs.GetInt("TotalDataPoints", 0);
        maxHpLevel = PlayerPrefs.GetInt("MaxHpLevel", 0);
        dmgLevel = PlayerPrefs.GetInt("DmgLevel", 0);
        atkSpdLevel = PlayerPrefs.GetInt("AtkSpdLevel", 0);
        critChanceLevel = PlayerPrefs.GetInt("CritChanceLevel", 0);
        critDmgLevel = PlayerPrefs.GetInt("CritDmgLevel", 0);
        defLevel = PlayerPrefs.GetInt("DefLevel", 0);
        moveSpdLevel = PlayerPrefs.GetInt("MoveSpdLevel", 0);
    }
}
