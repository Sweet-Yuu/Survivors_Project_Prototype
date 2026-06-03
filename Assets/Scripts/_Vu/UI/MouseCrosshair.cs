using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCrosshair : MonoBehaviour
{
    private Camera mainCam;
    private SpriteRenderer sr;

    private void Awake()
    {
        mainCam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
       
        bool isUsingGamepad = Gamepad.current != null &&
                             (Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.1f ||
                              Gamepad.current.leftStick.ReadValue().sqrMagnitude > 0.1f);

        sr.enabled = !isUsingGamepad;

        if (sr.enabled)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;
            transform.position = worldPos;
        }
    }
}