
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputDetection : MonoBehaviour
{
    public UnityEvent OnInputDetected;

    public InputActionReference actionButton;

    private void OnEnable()
    {

        actionButton.action.started += OnActionPressed;
        
        actionButton.action.canceled += OnActionReleased;
      
        actionButton.action.Enable();

        
    }

    private void OnActionPressed(InputAction.CallbackContext context)
    {
        OnInputDetected.Invoke();
    }

    private void OnActionReleased(InputAction.CallbackContext context)
    {
        
    }

    private void OnDisable()
    {
        
        actionButton.action.started -= OnActionPressed;
        actionButton.action.canceled -= OnActionReleased;
    }


}
