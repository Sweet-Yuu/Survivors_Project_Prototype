using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    [Header("Custom Scene")]
    public string[] scenePool;
    [Header("UI")]
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
            LoadRandomScene();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
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
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
    }
    void LoadRandomScene()
    {
        if (scenePool.Length == 0)
        {
            Debug.LogWarning("Scene pool is empty. Please add scene names to the pool.");
            return;
        }
        int randomIndex = Random.Range(0, scenePool.Length);
        string sceneName = scenePool[randomIndex];
        SceneManager.LoadScene(sceneName);
    }
}

