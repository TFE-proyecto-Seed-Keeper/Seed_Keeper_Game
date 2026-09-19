using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorControlCheck : MonoBehaviour
{
    [SerializeField]
    GameObject iconKeyboard, iconJoystick;
    private void OnEnable()
    {
        iconKeyboard.SetActive(!DetectJoystick());
        iconJoystick.SetActive(DetectJoystick());

    }

    public bool DetectJoystick()
    {
        var gamepads = Gamepad.all;

        if (gamepads.Count > 0)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
