using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class DamageListener : MonoBehaviour
{
    public UnityEvent OnDamage;

    private void OnEnable()
    {
        HealthSystem.OnDamage += OnPlayerDamage;
    }
    private void OnDisable()
    {
        HealthSystem.OnDamage -= OnPlayerDamage;
    }

    private void OnPlayerDamage(float damage)
    {
        OnDamage.Invoke();

        print("Damage recieved: " + damage);

        //GetComponent<TextParticleEmitter>()?.EmitDamageText(damage);

        GetComponentInChildren<TextsPulling>()?.ShowText("-" + damage.ToString(), false);

    }

    
}
