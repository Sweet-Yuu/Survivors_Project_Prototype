using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Scene Settings")]
    public string targetSceneName;

    [Header("UI Settings")]
    public GameObject interactionUI;

    private bool isPlayerInRange = false;

    private void Start()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Player.Instance.InputHandler.interactInput)
        {
            Player.Instance.InputHandler.UseInteractInput();
            OpenConfirmationCanvas();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionUI != null)
            {
                interactionUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }

            if (ConfirmationManager.Instance != null)
            {
                ConfirmationManager.Instance.OnCancelButtonPressed();
            }
        }
    }

    private void OpenConfirmationCanvas()
    {
        if (ConfirmationManager.Instance != null)
        {
            ConfirmationManager.Instance.OpenConfirmation(this);
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
        else
        {
            ConfirmLoadScene();
        }
    }

    public void ConfirmLoadScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(targetSceneName);
    }

    public void CancelLoadScene()
    {
        if (isPlayerInRange && interactionUI != null)
        {
            interactionUI.SetActive(true);
        }
    }
}