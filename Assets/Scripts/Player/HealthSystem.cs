using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamage, IHealable
{
    [SerializeField] private PlayerHealthSO playerHealth;

    public UnityEvent<float, float> OnHealthChanged;
    public UnityEvent onDeath;

    private void Start()
    {
        playerHealth.Initialize();
        OnHealthChanged?.Invoke(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    public void Heal(float amount)
    {
        playerHealth.Heal(amount);
        OnHealthChanged?.Invoke(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    public void ReceiveDamage(float damage)
    {
        playerHealth.ReceiveDamage(damage);
        OnHealthChanged?.Invoke(playerHealth.CurrentHealth, playerHealth.MaxHealth);

        if (playerHealth.IsDeath())
        {
            onDeath?.Invoke();
        }
    }
}
