using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class VisualInputDetection : MonoBehaviour
{
    [SerializeField]
    string AttackName;

    [SerializeField]
    Sprite pressSpriteButton, normalSpriteButton;

    

    [SerializeField]
    TextMeshProUGUI attackNameText;

    public InputActionReference PlayerInputAction;

    JoystickDetection joystickDetection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joystickDetection = GetComponentInParent<JoystickDetection>();
        attackNameText.text = AttackName;
    }

   
}
