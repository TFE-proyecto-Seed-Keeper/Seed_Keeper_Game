using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Seed Keeper/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Scene settings")]
    public string mainSceneName;

    [Header("Procedural Generation Settings")]
    public bool useSeed;
    public int seed;

    [Header("Level Settings")]
    public bool firstLoad;
}
