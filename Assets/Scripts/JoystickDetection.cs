
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JoystickDetection : MonoBehaviour
{
    [SerializeField]
     Sprite pad, keyboard;

     [SerializeField]
     Image iconImage;

    // Update is called once per frame
    void Update()
    {
        var gamepads = Gamepad.all;
        if(gamepads.Count>0)
        {
            iconImage.sprite = pad;
        }
        else
        {
            iconImage.sprite = keyboard;
        }
    }
}
