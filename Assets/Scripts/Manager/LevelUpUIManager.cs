using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    //public PlayerController player;

    // Hàm gọi khi bấm thẻ 1
    public void Update1()
    {
        // Logic tăng damage cho player
        ResumeGame();
    }

    public void Update2()
    {
        // Logic tăng tốc đánh cho player
        
    }

    public void Update3()
    {
        // Logic tăng phần trăm máu tối đa
    }

    private void ResumeGame()
    {
        gameObject.SetActive(false);  // Ẩn cái LevelUpPanel
        Time.timeScale = 1f;
    }

}
