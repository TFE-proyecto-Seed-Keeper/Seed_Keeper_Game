
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionButton : MonoBehaviour
{
    [SerializeField]
    InputActionReference _action;
    
    public UnityEvent actionPressed;

    private void OnEnable()
    {
        _action.action.started += SetActionStart;
        _action.action.Enable();
    }

    private void SetActionStart(InputAction.CallbackContext obj)
    {
        actionPressed.Invoke();
    }

    private void OnDisable()
    {
        _action.action.started -= SetActionStart;
    }
}
