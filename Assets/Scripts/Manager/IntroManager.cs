using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class IntroManager : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup transitionCanvasGroup;
    public CanvasGroup storyCanvasGroup;
    public Image storyDisplay;

    [Header("Story Content")]
    public List<Sprite> introSprites;
    public AudioClip introMusic; 

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 0.8f;
    [SerializeField] private string gameplaySceneName = "GamePlay";

    private int currentIdx = 0;
    private bool isTransitioning = false;

    private void Awake()
    {
        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.gameObject.SetActive(true);
            transitionCanvasGroup.alpha = 1f;
        }

        if (storyCanvasGroup != null) storyCanvasGroup.alpha = 0f;
    }

    private void Start()
    {
        if (introSprites.Count > 0)
        {
            storyDisplay.sprite = introSprites[0];
        }

        
        if (introMusic != null)
        {
            AudioManager.Instance.PlayMusic(introMusic, fadeDuration);
        }

        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                transitionCanvasGroup.gameObject.SetActive(false);
                if (storyCanvasGroup != null) storyCanvasGroup.DOFade(1f, fadeDuration);
            });
        }
    }

    public void OnScreenClicked()
    {
        if (isTransitioning) return;

        currentIdx++;

        if (currentIdx < introSprites.Count)
        {
            ShowNextImage();
        }
        else
        {
            FinishIntro();
        }
    }

    private void ShowNextImage()
    {
        isTransitioning = true;

        storyCanvasGroup.DOFade(0f, fadeDuration / 2).OnComplete(() =>
        {
            storyDisplay.sprite = introSprites[currentIdx];
            storyCanvasGroup.DOFade(1f, fadeDuration / 2).OnComplete(() =>
            {
                isTransitioning = false;
            });
        });
    }

    private void FinishIntro()
    {
        isTransitioning = true;

        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.gameObject.SetActive(true);
            transitionCanvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                SceneManager.LoadScene(gameplaySceneName);
            });
        }
        else
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
    }
}