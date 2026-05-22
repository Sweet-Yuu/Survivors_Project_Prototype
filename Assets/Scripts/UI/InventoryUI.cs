using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    
    public Image inventoryUI;

    private void Awake()
    {
        
        inventoryUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Player.Instance.InputHandler.informationInput)
        {
            
            inventoryUI.gameObject.SetActive(true);
        }
        else
        {
            inventoryUI.gameObject.SetActive(false);
        }

        
    }
}
