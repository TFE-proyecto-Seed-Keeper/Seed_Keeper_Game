using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Necesario para el Nuevo Input System
using System;
using System.Collections;

public class SkillUI : MonoBehaviour
{
    [Header("Referencias visuales")]
    public Animator skillAnimator;

    [Header("Configuración de Input")]
    // Te permite arrastrar la acción (ej. "Player/UseSkill") desde el Inspector
    public InputActionReference skillAction;

    [SerializeField]
    Image coldawnImage;

    [SerializeField]
    float skillDelay, skillScale;

    [SerializeField]
    bool skillEnabled;

    private void OnEnable()
    {
        // Nos suscribimos a los eventos cuando el objeto se activa
        // 'started' equivale al momento en que el botón baja (GetButtonDown)
        skillAction.action.started += OnSkillPressed;

        // 'canceled' equivale al momento en que el botón sube (GetButtonUp)
        skillAction.action.canceled += OnSkillReleased;

        // Opcional: Asegurarnos de que la acción esté habilitada para leerse
        // (A veces el PlayerInput ya lo hace por ti)
        skillAction.action.Enable();

        StartCoroutine(BlockSkill());
    }

    private void OnDisable()
    {
        // Es obligatorio desuscribirse cuando el objeto se desactiva
        // para evitar errores de memoria o referencias perdidas
        skillAction.action.started -= OnSkillPressed;
        skillAction.action.canceled -= OnSkillReleased;
    }

    // Este método se dispara automáticamente al presionar
    private void OnSkillPressed(InputAction.CallbackContext context)
    {
        if (!skillEnabled)
            return;

        skillAnimator.SetTrigger("Pressed");
        // LanzaHabilidad();

        
    }

    // Este método se dispara automáticamente al soltar
    private void OnSkillReleased(InputAction.CallbackContext context)
    {
        if (!skillEnabled)
            return;

        skillAnimator.SetTrigger("Normal");

        StartCoroutine(BlockSkill());
    }

    IEnumerator BlockSkill()
    {
        skillEnabled = false;

        coldawnImage.fillAmount = 1;

        float amounScale = skillScale / skillDelay;

        for (float i = 0; i < skillDelay; i+= skillScale)
        {
            coldawnImage.fillAmount -= amounScale;
            yield return new WaitForSeconds(skillScale);
        }

        coldawnImage.fillAmount = 0;

        skillEnabled = true;
    }
}