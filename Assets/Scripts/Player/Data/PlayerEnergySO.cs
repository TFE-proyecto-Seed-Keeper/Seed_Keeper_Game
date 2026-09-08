using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEnergy", menuName = "Seed Keeper/Player Energy")]
public class PlayerEnergySO : PlayerAttributeSO
{
    [SerializeField] private int maxValue = 7;
    [SerializeField] private float regenerationRate = 2f;

    [NonSerialized] private int currentValue;
    [NonSerialized] private static readonly string energyKey = "_Player_Energy_";
    [NonSerialized] private float regenerationTime = 0f;

    public void Restart()
    {
        currentValue = maxValue;
        regenerationTime = 0f;
        Save();
    }

    public void Initialize()
    {
        regenerationTime = 0f;
        currentValue = Load();

        if (currentValue <= 0)
        {
            currentValue = maxValue;
        }

        TriggerEvent(currentValue, maxValue);
    }

    public bool TrySpend(int amount)
    {
        if (CanSpend(amount))
        {
            currentValue -= amount;
            Update();
            TriggerEvent(currentValue, maxValue);

            return true;
        }

        return false;
    }

    public bool Regenerate(float deltaTime)
    {
        if (currentValue >= maxValue)
        {
            return false;
        }

        regenerationTime += deltaTime;

        if(regenerationTime < regenerationRate)
        {
            return false;
        }

        regenerationTime = 0f;
        currentValue += 1;
        Update();
        TriggerEvent(currentValue, maxValue);

        return true;

    }

    private void Update()
    {
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(energyKey, currentValue);
        PlayerPrefs.Save();
    }

    private int Load() => PlayerPrefs.GetInt(energyKey, 0);

    private bool CanSpend(int amount) => currentValue >= amount;
}
