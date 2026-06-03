using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerStatTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Tooltip Panel")]
    public CanvasGroup statsPanelGroup;
    public PlayerStatUI statUI; 

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 0.2f;

    private void Awake()
    {
        if (statsPanelGroup != null)
        {
            statsPanelGroup.alpha = 0f;
            statsPanelGroup.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (statsPanelGroup != null)
        {
            
            if (statUI != null) statUI.RefreshUI();

            statsPanelGroup.DOKill();
            statsPanelGroup.gameObject.SetActive(true);
            statsPanelGroup.DOFade(1f, fadeDuration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (statsPanelGroup != null)
        {
            statsPanelGroup.DOKill();
            statsPanelGroup.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                statsPanelGroup.gameObject.SetActive(false);
            });
        }
    }
}