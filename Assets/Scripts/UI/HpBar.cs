using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Image hpSlider;
    public Image hpFade;

    public float fadeSpeed = 0.2f;


    private void Start()
    {
        if (hpSlider == null)
        {
            Debug.LogError("HpBar: No Slider component assigned.");
        }
        if (Player.Instance != null && Player.Instance.Health != null)
        {
            Player.Instance.Health.OnHealthChanged += UpdateHpBar;
        }

        UpdateHpBar();
    }
    private void OnDestroy()
    {
        if (Player.Instance != null && Player.Instance.Health != null)
        {
            Player.Instance.Health.OnHealthChanged -= UpdateHpBar;
        }
    }

    public void UpdateHpBar()
    {
        if (hpSlider != null)
        {
            hpSlider.fillAmount = Player.Instance.Health.CurrentHealth / Player.Instance.PlayerData.maxHealth;
            StartCoroutine(HpBarFade());
        }
    }

    IEnumerator HpBarFade()
    {
        while(hpFade.fillAmount != hpSlider.fillAmount)
        {
            hpFade.fillAmount = Mathf.MoveTowards(hpFade.fillAmount, hpSlider.fillAmount, Time.deltaTime * fadeSpeed);
            yield return null;
        }
    }
}
