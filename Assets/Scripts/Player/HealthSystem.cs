using System;
using UnityEngine;
//using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamage, IHealable
{
    [SerializeField] private PlayerHealthSO playerHealth;

    public static event Action<float> OnDead;

    //public UnityEvent onDeath;

    private void Start()
    {
        playerHealth.Initialize();
    }

    public void Heal(float amount)
    {
        playerHealth.Heal(amount);
    }

    public void ReceiveDamage(float damage)
    {
        playerHealth.ReceiveDamage(damage);

        if (playerHealth.IsDeath())
        {

            OnDead?.Invoke(playerHealth.CurrentValue);
        }
    }
}
