using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCrosshair : MonoBehaviour
{
    private Camera mainCam;
    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 mousePos =
            Mouse.current.position.ReadValue();

        Vector3 worldPos =
            mainCam.ScreenToWorldPoint(mousePos);

        worldPos.z = 0f;
        transform.position = worldPos;
    }
}