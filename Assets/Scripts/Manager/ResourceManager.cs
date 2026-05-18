using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [Header("Level")]
    public int currentLevel = 1;
    public int expToNextLevel = 100;

    [Header("Kinh nghiệm (EXP")]
    public int currentExp = 0; 
    
    [Header ("Gold")]
    public int goldCollectedThisStage = 0;   // Vàng kiếm được theo từng màn 
    public int totalGold = 0;                // Tổng số vàng sau khi qua các màn
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            
        }
    }
    

    public void AddExp(int amount)
    {
        currentExp += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (currentLevel >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            currentLevel++;
            expToNextLevel += 50;    //Tăng độ khó cho từng màn
        }
    }

    public void AddGold(int amount)
    {
        goldCollectedThisStage += amount;
    }

    public void StageCompleted()
    {
        totalGold += goldCollectedThisStage;
        goldCollectedThisStage = 0;
    }

    
}
