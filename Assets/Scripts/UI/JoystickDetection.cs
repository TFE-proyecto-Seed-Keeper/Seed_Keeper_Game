
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JoystickDetection : MonoBehaviour
{
    [SerializeField]
     Sprite pad, keyboard;

     [SerializeField]
     Image iconImage;

    public bool isJoystick = false;

    // Update is called once per frame
    void Update()
    {
        var gamepads = Gamepad.all;
        if(gamepads.Count>0)
        {
            iconImage.sprite = pad;
            isJoystick = true;
        }
        else
        {
            iconImage.sprite = keyboard;
            isJoystick = false;
        }
    }

    public bool DetectJoystick()
    {
        return isJoystick;
    }
}
