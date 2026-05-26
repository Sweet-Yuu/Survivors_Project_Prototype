using DG.Tweening;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [Header("UI Elements")]
    public Image expSlider;
    public TextMeshProUGUI levelText; 
    public TextMeshProUGUI expText;   

    private Sequence _expSequence;
    private float _lastFillAmount = 0f;

    private void Start()
    {
        if (expSlider == null) Debug.LogError("ExpBar: No Slider component assigned.");
        expSlider.enabled = true;
        expSlider.fillAmount = 0f;
        _lastFillAmount = 0f;

        if (Player.Instance != null && Player.Instance.Experience != null)
        {
            Player.Instance.Experience.OnExpChanged += UpdateExpBar;
        }
        UpdateExpBar();
    }

    private void UpdateExpBar()
    {
        if (expSlider == null || Player.Instance == null || Player.Instance.Experience == null) return;

        float current = Player.Instance.Experience.currentExp;
        float targetMax = Player.Instance.Experience.expToNextLevel;
        float targetFillAmount = current / targetMax;
        int currentLevel = Player.Instance.Experience.currentLevel; 

       
        if (levelText != null) levelText.text = $"Lv. {currentLevel}";
        if (expText != null) expText.text = $"{current} / {targetMax}";

        
        if (_expSequence != null && _expSequence.IsActive())
        {
            _expSequence.Kill(true);
        }
        _expSequence = DOTween.Sequence();

        if (targetFillAmount < _lastFillAmount)
        {
            _expSequence.Append(expSlider.DOFillAmount(1f, 0.2f).SetEase(Ease.OutCubic));
            _expSequence.AppendCallback(() => expSlider.fillAmount = 0f);
            _expSequence.Append(expSlider.DOFillAmount(targetFillAmount, 0.3f).SetEase(Ease.OutCubic));
        }
        else
        {
            _expSequence.Append(expSlider.DOFillAmount(targetFillAmount, 0.25f).SetEase(Ease.OutCubic));
        }

        _lastFillAmount = targetFillAmount;
    }

    private void OnDestroy()
    {
        if (Player.Instance != null && Player.Instance.Experience != null)
        {
            Player.Instance.Experience.OnExpChanged -= UpdateExpBar;
        }
        if (_expSequence != null) _expSequence.Kill();
    }
}