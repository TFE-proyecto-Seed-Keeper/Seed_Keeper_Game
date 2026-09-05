using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Seed Keeper/Player Health")]
public class PlayerHealthSO : ScriptableObject
{
    [SerializeField] private float maxHealth = 100f;
    [NonSerialized] private float currentHealth;
    [NonSerialized] private static readonly string healthKey = "_Player_Health_";

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    public void Restart()
    {
        currentHealth = maxHealth;
        Save();
    }

    public void Initialize()
    {
        currentHealth = Load();

        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }
    }

    public void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
        Update();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        Update();
    }

    public bool IsDeath() => currentHealth <= 0;

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(healthKey, currentHealth);
        PlayerPrefs.Save();
    }

    private float Load() => PlayerPrefs.GetFloat(healthKey, 0);
}
