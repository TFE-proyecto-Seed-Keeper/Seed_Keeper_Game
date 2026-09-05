using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Seed Keeper/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Scene settings")]
    public string mainSceneName;

    [Header("Seed settings")]
    public bool useSeed;
    public int seed;
}
