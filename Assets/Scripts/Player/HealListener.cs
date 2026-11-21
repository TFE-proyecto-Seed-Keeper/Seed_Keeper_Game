using System;
using UnityEngine;

using UnityEngine.Events;

public class HealListener : MonoBehaviour
{
    public UnityEvent OnHeal;
    private void OnEnable()
    {
        HealthSystem.OnHeal += OnPlayerHeal;
    }

    private void OnDisable()
    {
        HealthSystem.OnHeal -= OnPlayerHeal;
    }

    private void OnPlayerHeal(float healAmount)
    {
        OnHeal?.Invoke();

        print("Heal recieved: " + healAmount);
        //GetComponent<TextParticleEmitter>()?.EmitDamageText(healAmount);

        GetComponentInChildren<TextsPulling>()?.ShowText("+" + healAmount.ToString(), true);

    }

    
}
