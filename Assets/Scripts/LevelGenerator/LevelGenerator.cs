using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private GameSettingsSO gameSettings;
    [SerializeField] private LevelSettingsSO levelSettings;
    [SerializeField] private Animator levelLoaderAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private int xOffset = 50;

    private SectionInfo[] sections;
    private Dictionary<SectionType, SectionInfo[]> sectionsByType;
    private bool hasSectionsToGenerate;
    private Vector3 currentSectionPosition;

    private bool HasSectionsToGenerate()
    {
        return levelSettings.levelMapSections.Length > 0
            || levelSettings.sectionPrefabs.Length > 0;
    }

    private void Awake()
    {
        hasSectionsToGenerate = HasSectionsToGenerate();

        if (!hasSectionsToGenerate)
        {
            Debug.Log("LevelSettings has not sectionPrefabs nor levelMapSections set");
            return;
        }

        SetUpSeed();

        currentSectionPosition = transform.position;

        sections = new SectionInfo[levelSettings.levelMapSections.Length];

        sectionsByType = (levelSettings.sectionPrefabs ?? new LevelSection[0])
            .Select(x => new SectionInfo(x))
            .GroupBy(x => x.type)
            .ToDictionary(x => x.Key, x => x.ToArray());
    }

    private void SetUpSeed()
    {
        var gameSeed = gameSettings.seed;

        if (!gameSettings.useSeed)
        {
            gameSeed = Random.Range(0, 1_000_000);
        }

        Random.InitState(gameSeed);
        Debug.Log($"Random Seed: {gameSeed}");
    }

    private void Start()
    {
        if(!hasSectionsToGenerate)
        {
            return;
        }

        bool isGenerated = Generate();

        if (isGenerated)
        {
            SetupSections();
            StartLevel();
        }
    }

    private void StartLevel()
    {
        SectionInfo startSection = sections[0];

        startSection.sectionPrefab.gameObject.SetActive(true);

        startSection.sectionPrefab.InitSection(player);
    }

    private bool Generate()
    {
        bool isGenerated = true;
        PathDirectionType? nextDirection = levelSettings.startPathDirection;

        for (int i = 0; i < levelSettings.levelMapSections.Length; i++)
        {
            SectionType type = levelSettings.levelMapSections[i];

            if(!sectionsByType.TryGetValue(type, out SectionInfo[] availableSections))
            {
                isGenerated = false;
                Debug.Log($"No sections found for '{type}' section type");
                break;
            }

            //bool shouldHaveEndPath = i < (levelSettings.levelMapSections.Length - 1);

            //nextDirection = AddLevelSection(availableSections, i, nextDirection.Value, shouldHaveEndPath);

            nextDirection = AddLevelSection(availableSections, i, nextDirection.Value, true);

            if (nextDirection == null)
            {
                break;
            }

            nextDirection = nextDirection.Value.GetOpposedDirection();
        }

        return isGenerated;
    }

    private void SetupSections()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            SectionInfo currentSection = sections[i];

            if (i > 0)
            {
                currentSection.sectionPrefab.SetPreviousSection(sections[i - 1].sectionPrefab, false);
            }

            if (i < (sections.Length - 1))
            {
                currentSection.sectionPrefab.SetNextSection(sections[i + 1].sectionPrefab);
            }

            if (currentSection.sectionPrefab.HasAlternativePath)
            {
                currentSection.sectionPrefab.SetAlternativeSection(currentSection.alternativeSectionInfo.sectionPrefab);
                currentSection.alternativeSectionInfo.sectionPrefab.SetPreviousSection(currentSection.sectionPrefab, true);
            }
        }
    }

    private PathDirectionType? AddLevelSection(SectionInfo[] availableSections, int sectionIndex, PathDirectionType nextDirection, bool shouldHaveEndPath)
    {
        SectionInfo section = GetSection(availableSections, nextDirection, shouldHaveEndPath);

        if (shouldHaveEndPath && section.sectionPrefab.HasAlternativePath)
        {
            SectionInfo alternativeSection = GetAlternativeSection(section.alternativeDirection.GetOpposedDirection());

            section.alternativeSectionInfo = alternativeSection;
        }

        sections[sectionIndex] = section;

        return section.endDirection;
    }

    private SectionInfo GetSection(SectionInfo[] availableSections, PathDirectionType nextDirection, bool shouldHaveEndPath, bool? shouldHaveAlternativePath = null)
    {
        SectionInfo[] validSections = availableSections
                    .Where(x => x.startDirection == nextDirection
                                && x.sectionPrefab.HasEndPath == shouldHaveEndPath
                                && (shouldHaveAlternativePath == null || x.sectionPrefab.HasAlternativePath == shouldHaveAlternativePath))
                    .ToArray();

        if(validSections.Length == 0)
        {
            return null;
        }

        int index = Random.Range(0, validSections.Length);

        var section = Instantiate(validSections[index].sectionPrefab, currentSectionPosition, Quaternion.identity, transform)
            .GetComponent<LevelSection>();

        section.name += $"_{section.SectionType}";
        section.gameObject.SetActive(false);

        section.Setup(gameSettings, levelLoaderAnimator);

        currentSectionPosition.x += xOffset;

        return new (section);
    }

    private SectionInfo GetAlternativeSection(PathDirectionType nextDirection)
    {
        if(levelSettings.typesForAlternativeSections.Length == 0)
        {
            return null;
        }

        SectionInfo[] validSections = levelSettings.typesForAlternativeSections
            .Select(type => sectionsByType.TryGetValue(type, out SectionInfo[] result) ? result : new SectionInfo[0])
            .SelectMany(x => x)
            .ToArray();

        if (validSections.Length == 0)
        {
            return null;
        }

        return GetSection(validSections, nextDirection, false, false);
    }
}
