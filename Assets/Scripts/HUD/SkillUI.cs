using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el Nuevo Input System
using UnityEngine.UI;
using System.Collections.Generic;


public class SkillUI : MonoBehaviour
{
    [Header("Referencias visuales")]
    public Animator skillAnimator;

    [Header("Configuración de Input")]
    // Te permite arrastrar la acción (ej. "Player/UseSkill") desde el Inspector
    public InputActionReference skillAction;

    public enum AttackType
    { 
        None,
        melee,
        range,
        area
    }

    public AttackType attackType ;

    [SerializeField]
    GameObject padIcon, KeyIcon;

    [SerializeField]
    Image coldawnImage;

    [SerializeField]
    float skillDelay, skillScale;

    [SerializeField]
    bool skillEnabled;

    public List<IAttacks> AllIAttacks = new List<IAttacks>();

    public void TakeDamage(float amount)
    {
        Debug.Log($"{gameObject.name} recibió {amount} de daño global!");
    }

    private void OnEnable()
    {
        FindIattacks();
        // Nos suscribimos a los eventos cuando el objeto se activa
        // 'started' equivale al momento en que el botón baja (GetButtonDown)
        skillAction.action.started += OnSkillPressed;

        // 'canceled' equivale al momento en que el botón sube (GetButtonUp)
        skillAction.action.canceled += OnSkillReleased;

        // Opcional: Asegurarnos de que la acción estó habilitada para leerse
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

    // Este m�todo se dispara autom�ticamente al presionar
    private void OnSkillPressed(InputAction.CallbackContext context)
    {
        if (!skillEnabled)
            return;
       
        skillAnimator.SetTrigger("Pressed");
        
        StartAttack();

        StartCoroutine(BlockSkill());


    }

    // Este método se dispara automáticamente al soltar
    private void OnSkillReleased(InputAction.CallbackContext context)
    {

        skillAnimator.SetTrigger("Normal");
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

    public void SetJoystick()
    {
        padIcon.SetActive(true);
        KeyIcon.SetActive(false);
    }

    public void SetKeyboard()
    {
        padIcon.SetActive(false);
        KeyIcon.SetActive(true);
    }


    void StartAttack()
    {
        
        foreach (var attack in AllIAttacks)
        {
            switch (attackType)
            {
                case AttackType.None:
                    break;

                case AttackType.melee:
                    attack.SetMeleeAttack();
                    break;

                case AttackType.range:
                    attack.SetRangettack();
                    break;

                case AttackType.area:
                    attack.SetAreaAttack();
                    break;
            }
            
        }
        
    }

    void FindIattacks()
    {
        var monos = FindObjectsByType<MonoBehaviour>();
        foreach (var mb in monos)
        {
            if (mb is IAttacks attack)
            {
               AllIAttacks.Add(attack);
            }
        }
    }
}