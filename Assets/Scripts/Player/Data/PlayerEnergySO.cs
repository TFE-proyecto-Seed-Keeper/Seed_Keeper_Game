using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEnergy", menuName = "Seed Keeper/Player Energy")]
public class PlayerEnergySO : ScriptableObject
{
    [SerializeField] private int maxEnergyCells = 7;
    [SerializeField] private float regenerationRate = 2f;

    [NonSerialized] private int currentEnergyCells;
    [NonSerialized] private static readonly string energyKey = "_Player_Energy_";
    [NonSerialized] private float regenerationTime = 0f;

    public int MaxEnergyCells => maxEnergyCells;
    public int CurrentEnergyCells => currentEnergyCells;

    public void Restart()
    {
        currentEnergyCells = maxEnergyCells;
        regenerationTime = 0f;
        Save();
    }

    public void Initialize()
    {
        regenerationTime = 0f;
        currentEnergyCells = Load();

        if (currentEnergyCells <= 0)
        {
            currentEnergyCells = maxEnergyCells;
        }
    }

    public bool TrySpend(int amount)
    {
        if (CanSpend(amount))
        {
            currentEnergyCells -= amount;
            Update();

            return true;
        }

        return false;
    }

    public bool Regenerate(float deltaTime)
    {
        if (currentEnergyCells >= maxEnergyCells)
        {
            return false;
        }

        regenerationTime += deltaTime;

        if(regenerationTime < regenerationRate)
        {
            return false;
        }

        regenerationTime = 0f;
        currentEnergyCells += 1;
        Update();

        return true;

    }

    private void Update()
    {
        currentEnergyCells = Mathf.Clamp(currentEnergyCells, 0, maxEnergyCells);
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(energyKey, currentEnergyCells);
        PlayerPrefs.Save();
    }

    private int Load() => PlayerPrefs.GetInt(energyKey, 0);

    private bool CanSpend(int amount) => currentEnergyCells >= amount;
}
