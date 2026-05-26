using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{
    [Header("UI Panels")]
    public CanvasGroup transitionCanvasGroup;
    public GameObject pressToPlayPanel;
    public GameObject gameMenuPanel;
    public GameObject settingsPanel;

    [Header("Fancy Animation Elements")]
    public RectTransform menuTitle;
    public RectTransform buttonsContainer;

    [Header("Audio Settings")]
    public AudioClip menuBackgroundMusic; 

    [Header("Configuration")]
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float moveDuration = 0.4f;

    private Vector2 titleOriginalPos;
    private Vector2 buttonsOriginalPos;

    private void Awake()
    {
        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.alpha = 0f;
            transitionCanvasGroup.gameObject.SetActive(false);
        }

        pressToPlayPanel.SetActive(true);
        gameMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        if (menuTitle != null) titleOriginalPos = menuTitle.anchoredPosition;
        if (buttonsContainer != null) buttonsOriginalPos = buttonsContainer.anchoredPosition;
    }

    private void Start()
    {
        if (menuBackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(menuBackgroundMusic, 1.5f);
        }
    }

    public void OnPressToPlayClicked()
    {
        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.gameObject.SetActive(true);

            transitionCanvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                pressToPlayPanel.SetActive(false);
                gameMenuPanel.SetActive(true);

                if (menuTitle != null)
                {
                    menuTitle.anchoredPosition = new Vector2(titleOriginalPos.x, titleOriginalPos.y + 200f);
                    menuTitle.DOAnchorPos(titleOriginalPos, moveDuration).SetEase(Ease.OutCubic);
                }

                if (buttonsContainer != null)
                {
                    buttonsContainer.anchoredPosition = new Vector2(buttonsOriginalPos.x + 300f, buttonsOriginalPos.y);
                    buttonsContainer.DOAnchorPos(buttonsOriginalPos, moveDuration).SetEase(Ease.OutCubic);

                    CanvasGroup btnCG = buttonsContainer.GetComponent<CanvasGroup>();
                    if (btnCG != null)
                    {
                        btnCG.alpha = 0f;
                        btnCG.DOFade(1f, moveDuration);
                    }
                }

                transitionCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
                {
                    transitionCanvasGroup.gameObject.SetActive(false);
                });
            });
        }
        else
        {
            pressToPlayPanel.SetActive(false);
            gameMenuPanel.SetActive(true);
        }
    }

    public void OnStartClicked()
    {
       
        AudioManager.Instance.StopMusic(fadeDuration);

        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.gameObject.SetActive(true);
            transitionCanvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                SceneManager.LoadScene("GameIntro");
            });
        }
        else
        {
            SceneManager.LoadScene("GameIntro");
        }
    }

    public void OnSettingsClicked()
    {
        if (settingsPanel != null)
        {
            gameMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
    }

    public void OnCloseSettingsClicked()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            gameMenuPanel.SetActive(true);
        }
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}