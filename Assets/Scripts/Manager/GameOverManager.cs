using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI & Animation")]
    public GameObject gameOverPanel;
    public GameObject gameTransitionPanel;
    public Animator transitionAnimator;

    [Header("Settings")]
    public float animationDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        gameOverPanel.SetActive(false);
        gameTransitionPanel.SetActive(false);

    }
    public void GameOver()
    {
        
        StartCoroutine(FadeInGameOverPanel());
        
    }
    private IEnumerator FadeInGameOverPanel()
    {
        gameTransitionPanel.SetActive(true);
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("FadeIn");
        }
        
        yield return new WaitForSecondsRealtime(animationDuration);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
    public void RestartGame()
    {
        Player.IsDead = false;
        StartCoroutine(FadeOutAndRestart());
    }
    private IEnumerator FadeOutAndRestart()
    {
        gameOverPanel.SetActive(false);

        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("FadeOut");
        }
        yield return new WaitForSecondsRealtime(animationDuration);

        gameTransitionPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


}
