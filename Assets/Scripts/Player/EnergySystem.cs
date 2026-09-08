using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private PlayerEnergySO playerEnergy;

    private void Start()
    {
        playerEnergy.Initialize();
    }

    private void Update()
    {
        playerEnergy.Regenerate(Time.deltaTime);
    }

    public bool TrySpendEnergy(int amount) => playerEnergy.TrySpend(amount);
}
