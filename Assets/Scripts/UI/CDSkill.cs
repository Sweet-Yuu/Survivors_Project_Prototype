using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CDSkill : MonoBehaviour
{
    public Image cdImage;
    public TextMeshProUGUI cdNumber;

    private void Start()
    {
        cdImage.fillAmount = 0;
        if (cdNumber != null) cdNumber.gameObject.SetActive(false);
        if (Player.Instance != null && Player.Instance.Dash != null)
        {
            Player.Instance.Dash.OnDashEvent += StartDashCD;
        }

    }

    private void OnDestroy()
    {
        if (Player.Instance != null && Player.Instance.Dash != null)
        {
            Player.Instance.Dash.OnDashEvent -= StartDashCD;
        }
    }

    private void StartDashCD()
    {
        float cooldownTime = Player.Instance.PlayerData.dashCooldown;
        StartCoroutine(CD(cooldownTime));
    }

    private IEnumerator CD(float cdTime)
    {
        if (cdNumber != null) cdNumber.gameObject.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < cdTime)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = cdTime - elapsedTime;
            cdImage.fillAmount = 1 - (elapsedTime / cdTime);
            if (cdNumber != null) cdNumber.text = Mathf.Max(remainingTime, 0f).ToString("F1");
            yield return null;
        }
        cdImage.fillAmount = 0;
        if (cdNumber != null) cdNumber.gameObject.SetActive(false);
    }


}
