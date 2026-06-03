using UnityEngine;

public class MenuCursorController : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }
}