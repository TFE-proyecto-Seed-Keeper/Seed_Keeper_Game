using UnityEngine;
using UnityEngine.Events;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private PlayerEnergySO playerEnergy;

    public UnityEvent<float, float> OnEnergyChanged;

    private void Start()
    {
        playerEnergy.Initialize();
        TriggerEvent(true);
    }

    private void Update()
    {
        bool regenerated = playerEnergy.Regenerate(Time.deltaTime);

        TriggerEvent(regenerated);
    }

    public bool TrySpendEnergy(int amount)
    {
        var wasSpent = playerEnergy.TrySpend(amount);

        TriggerEvent(wasSpent);

        return wasSpent;
    }

    private void TriggerEvent(bool trigger)
    {
        if (trigger)
        {
            OnEnergyChanged?.Invoke(playerEnergy.CurrentEnergyCells, playerEnergy.MaxEnergyCells);
        }
    }
}
