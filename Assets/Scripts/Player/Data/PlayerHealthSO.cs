using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Seed Keeper/Player Health")]
public class PlayerHealthSO : PlayerAttributeSO
{
    [SerializeField] private float maxValue = 100f;

    [NonSerialized] private float currentValue;
    [NonSerialized] private static readonly string healthKey = "_Player_Health_";

    public float CurrentValue { get => currentValue; set => currentValue = value; }

    public void Restart()
    {
        CurrentValue = maxValue;
        Save();
    }

    public void Initialize()
    {
        CurrentValue = Load();

        if(CurrentValue <= 0)
        {
            CurrentValue = maxValue;
        }

        TriggerEvent(CurrentValue, maxValue);
    }

    public void ReceiveDamage(float amount)
    {
        CurrentValue -= amount;
        Update();
        TriggerEvent(CurrentValue, maxValue);
    }

    public void Heal(float amount)
    {
        CurrentValue += amount;
        Update();
        TriggerEvent(CurrentValue, maxValue);
    }

    public bool IsDeath() => CurrentValue <= 0;

    private void Update()
    {
        CurrentValue = Mathf.Clamp(CurrentValue, 0, maxValue);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(healthKey, CurrentValue);
        PlayerPrefs.Save();
    }

    private float Load() => PlayerPrefs.GetFloat(healthKey, 0);
}
