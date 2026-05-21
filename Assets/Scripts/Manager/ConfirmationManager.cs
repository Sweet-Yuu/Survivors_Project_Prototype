using UnityEngine;

public class ConfirmationManager : MonoBehaviour
{
    public static ConfirmationManager Instance;
    public GameObject uiPanel;
    private Portal currentActivePortal;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Cursor.visible = true;
    }

    private void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    public void OpenConfirmation(Portal portal)
    {
        currentActivePortal = portal;
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }

    public void OnConfirmButtonPressed()
    {
        if (currentActivePortal != null)
        {
            currentActivePortal.ConfirmLoadScene();
        }
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    public void OnCancelButtonPressed()
    {
        if (currentActivePortal != null)
        {
            currentActivePortal.CancelLoadScene();
        }
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
        currentActivePortal = null;
    }
}