using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 mouseScreenPosition;
    public Vector2 MouseDirection { get; private set; }
    public Vector2 movementInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool settingInput { get; private set; }


    private Camera cam;
    private Transform playerTransform;

    private void Awake()
    {
        cam = Camera.main;
        playerTransform = transform;
    }

    private void Update()
    {
        UpdateMouseDirection();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnMousePositionInput(InputAction.CallbackContext context)
    {
        mouseScreenPosition = context.ReadValue<Vector2>();
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
       AttackInput = context.ReadValueAsButton();
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        dashInput= context.ReadValueAsButton();
    }

    public void OnSettingInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            settingInput = true;
        }
       
        
    }

    public void UseSettingInput()
    {
        settingInput = false;
    }
    public void UseDashInput()
    {
        dashInput = false;
    }

    private void UpdateMouseDirection()
    {
        Vector3 mouseWorldPos =
            cam.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPos.z = 0f;

        MouseDirection =
            (mouseWorldPos - playerTransform.position).normalized;
    }
    
}