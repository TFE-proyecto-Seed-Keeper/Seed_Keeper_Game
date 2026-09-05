using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Seed Keeper/Level Settings")]
public class LevelSettingsSO : ScriptableObject
{
    [Header("Level Settings")]
    public LevelSection[] sectionPrefabs;
    [Tooltip("Select the sections in the order the level map will be created.")]
    public SectionType[] levelMapSections;

    [Tooltip("Position for the start spawn point in the first section of the level map.")]
    public PathDirectionType startPathDirection = PathDirectionType.Left;

    [Tooltip("Section types for alternative sections on the level map.")]
    public SectionType[] typesForAlternativeSections = new[] { SectionType.Enemy, SectionType.Loot };
}
