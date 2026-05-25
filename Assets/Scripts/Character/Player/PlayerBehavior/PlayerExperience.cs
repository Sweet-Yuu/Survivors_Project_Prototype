using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [Header("Level")]
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;


    [Header("Manager")]

    public UpgradeManager upgradeManager;

    public event Action OnExpChanged;
    public void GainExperience(int amount)
    {
        if (amount <= 0) return;
        
        currentExp += amount;

        
        while (currentExp >= expToNextLevel)
        {
            
            OnExpChanged?.Invoke();

            LevelUp();
        }
        OnExpChanged?.Invoke();
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExp -= expToNextLevel;


        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.2f);


        if (upgradeManager != null)
        {
            upgradeManager.ShowUpgradeSelection();
        }
    }
}
