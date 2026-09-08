using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Seed Keeper/Player Health")]
public class PlayerHealthSO : PlayerAttributeSO
{
    [SerializeField] private float maxValue = 100f;

    [NonSerialized] private float currentValue;
    [NonSerialized] private static readonly string healthKey = "_Player_Health_";

    public void Restart()
    {
        currentValue = maxValue;
        Save();
    }

    public void Initialize()
    {
        currentValue = Load();

        if(currentValue <= 0)
        {
            currentValue = maxValue;
        }

        TriggerEvent(currentValue, maxValue);
    }

    public void ReceiveDamage(float amount)
    {
        currentValue -= amount;
        Update();
        TriggerEvent(currentValue, maxValue);
    }

    public void Heal(float amount)
    {
        currentValue += amount;
        Update();
        TriggerEvent(currentValue, maxValue);
    }

    public bool IsDeath() => currentValue <= 0;

    private void Update()
    {
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(healthKey, currentValue);
        PlayerPrefs.Save();
    }

    private float Load() => PlayerPrefs.GetFloat(healthKey, 0);
}
