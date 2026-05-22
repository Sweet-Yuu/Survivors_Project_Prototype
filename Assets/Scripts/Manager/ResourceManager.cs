using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [Header("VFX && Player")]
    public GameObject levelUPVFXPrefab;
    public Transform playerTransform;

    [Header("UI References")]
    public Slider expBar;
    public TextMeshProUGUI levelText;
    public GameObject levelUpPanel;
    public CardManager cardManager;

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
    private void Start()
    {
        UpdateUI();
        if(levelUpPanel !=null) levelUpPanel.SetActive(false);
    }
    

    public void AddExp(int amount)
    {
        currentExp += amount;
        CheckLevelUp();
        UpdateUI(); // Cập nhật lại expBar mỗi khi nhận exp
    }

    private void CheckLevelUp()
    {
        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            currentLevel++;
            expToNextLevel += 50;    //Tăng độ khó cho từng màn
            TriggerLevelUpEvent();
        }
    }

    private void TriggerLevelUpEvent()
    {
        if (levelUPVFXPrefab != null && playerTransform != null)
        {
            Instantiate(levelUPVFXPrefab, playerTransform.position, Quaternion.identity);
        }
        // Dừng thời gian game
        Time.timeScale = 0;

        if (levelUpPanel != null) levelUpPanel.SetActive(true);

        if (cardManager != null) cardManager.StartLevelUpSelection();

    }

    public void ResumeGame()
    {
        //Hàm này sẽ được gọi sau khi người chơi chọn xong bài
        Time.timeScale = 1f;
        if (levelUpPanel != null) levelUpPanel.SetActive(false);
        CheckLevelUp();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (expBar != null)
        {
            expBar.maxValue=expToNextLevel;
            expBar.value = currentExp;

        }

        if (levelText != null)
        {
            levelText.text = "LV : " +currentLevel;
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
