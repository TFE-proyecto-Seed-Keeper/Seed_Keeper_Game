
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JoystickDetection : MonoBehaviour
{

    public bool isJoystick = false;
    bool lastState = true;

    

    // Update is called once per frame
    private void FixedUpdate()
    {
        DetectJoystick();
    }

    public void DetectJoystick()
    {
        var gamepads = Gamepad.all;
        
        if (gamepads.Count > 0)
        {
            
            if (lastState != isJoystick)
            {
                BroadcastMessage("SetJoystick", SendMessageOptions.DontRequireReceiver);
                print("Is Joystick");
                print("Pads detectados: "+gamepads.Count);
                foreach (var gamepad in gamepads)
                {
                    // Prints the name and device ID (e.g., "XboxControllerHID #0")
                    Debug.Log($"Gamepad found: {gamepad.displayName} (ID: {gamepad.deviceId})");
                }
                lastState = isJoystick;
            }
            isJoystick = true;
        }
        else
        {
            if (lastState != isJoystick)
            {
                BroadcastMessage("SetKeyboard", SendMessageOptions.DontRequireReceiver);
                print("Is Keyboard");
                lastState = isJoystick;
            }
            isJoystick = false;
        }
    }
}
