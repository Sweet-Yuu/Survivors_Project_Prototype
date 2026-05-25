using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 mouseScreenPosition;
    public Vector2 MouseDirection { get; private set; }
    public Vector2 movementInput { get; private set; }
   
    public bool AttackInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool interactInput { get; private set; }
    public bool informationInput { get; private set; }


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
        dashInput = context.ReadValueAsButton();
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            interactInput = true;
        }
        else if (context.canceled)
        {
            interactInput = false;
        }
    }
    public void OnInformationInput(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            informationInput = true;
        }
        else if (context.canceled)
        {
            informationInput = false;
        }
    }

    public void UseDashInput()
    {
        dashInput = false;
    }
    public void UseInteractInput()
    {
        interactInput = false;
    }
    public void UseInformationInput()
    {
        informationInput = false;
    }

    private bool UpdateMouseDirection()
    {
        
        
            
            Vector3 mouseWorldPos =
                            cam.ScreenToWorldPoint(mouseScreenPosition);

            mouseWorldPos.z = 0f;

            MouseDirection =
                (mouseWorldPos - playerTransform.position).normalized;
            return true;
        
    }


}