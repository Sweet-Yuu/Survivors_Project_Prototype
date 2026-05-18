using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 
    [Header("--- UI Panels ---")]
    // Khai báo biến này để kéo SettingPanel vào trong Inspector
    public GameObject settingPanel;


    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleSettingPanel(bool isActive)
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(isActive);
        }
       
    }
}
