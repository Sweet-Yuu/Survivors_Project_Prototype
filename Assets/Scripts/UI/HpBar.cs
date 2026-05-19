using TMPro;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public TextMeshProUGUI HpText;
    public GameObject HpBarHolder;
    

    private void Start()
    {
        if (HpBarHolder == null)
        {
            Transform foundTransform = transform.Find("HpBarHolder");
            if (foundTransform != null)
            {
                HpBarHolder = foundTransform.gameObject;
            }
            else
            {
                Debug.LogError("Not Found!");
            }
        }
    }
    private void Update()
    {
        UpdateHpBar();
    }
    public void UpdateHpBar()
    {
        if (HpText != null)
        {
            HpText.text = $"{Player.Instance.Health.CurrentHealth} / {Player.Instance.PlayerData.maxHealth}";
        }
    }
}
