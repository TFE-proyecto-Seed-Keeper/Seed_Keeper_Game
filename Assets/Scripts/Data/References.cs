using UnityEngine;

public class References : MonoBehaviour
{
    [SerializeField] private GameSettingsSO gameSettingsSO;
    [SerializeField] private SeedData seedData;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
}
