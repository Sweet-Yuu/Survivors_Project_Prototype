using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseGameManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale=0f;
        isPaused = true;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
