using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private GameObject inventoryUIPanel;
    private bool isUIActive = false;
    private void Awake()
    {
        
        if (inventoryUIPanel != null)
        {
            inventoryUIPanel.SetActive(false);
            isUIActive = false;
        }
    }

    private void Update()
    {
        
        if (Player.Instance.InputHandler.informationInput)
        {
            
            isUIActive = !isUIActive;

            inventoryUIPanel.SetActive(isUIActive);

            if (isUIActive)
            {
                Time.timeScale = 0f; 
            }
            else
            {
                Time.timeScale = 1f; 
            }

            Player.Instance.InputHandler.UseInformationInput();
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
