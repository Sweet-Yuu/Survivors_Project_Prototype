using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{
    public Image manaSlider;
    private Tween _cooldownTween;

    private void Start()
    {
        if (manaSlider != null)
        {

            manaSlider.enabled = true;
            manaSlider.fillAmount = 1f;
        }

        if (Player.Instance != null && Player.Instance.Dash != null)
        {
            Player.Instance.Dash.OnDashEvent += StartDashCD;
        }
    }
    private void StartDashCD()
    {
        if (manaSlider == null) return;
        float cooldownTime = Player.Instance.PlayerData.dashCooldown;

        if (_cooldownTween != null && _cooldownTween.IsActive())
        {
            _cooldownTween.Kill();
        }

        manaSlider.fillAmount = 0f;
        _cooldownTween = manaSlider.DOFillAmount(1f,cooldownTime)
            .SetEase(Ease.Linear);
    }
    private void OnDestroy()
    {
        if (Player.Instance != null && Player.Instance.Dash != null)
        {
            Player.Instance.Dash.OnDashEvent -= StartDashCD;
        }

        if (_cooldownTween != null) _cooldownTween.Kill();
    }
}
