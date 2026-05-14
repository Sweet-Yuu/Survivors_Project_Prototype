using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    
    public Vector2 movementInput { get; private set; }

    public void OnMovementInput(InputAction.CallbackContext context)
   {
      movementInput = context.ReadValue<Vector2>();
    }
}
