using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSection : MonoBehaviour
{
    [Header("Section Settings")]
    [SerializeField] private SectionType sectionType; 
    [SerializeField] private bool hasEndPath = true, hasAlternativePath = false;

    [Header("Path Settings")]
    [SerializeField] private Teleporter startTrigger;
    [SerializeField] private Teleporter endTrigger;
    [SerializeField] private Teleporter alternativeTrigger;

    [Header("Spawn Points")]
    [SerializeField] private Transform startSpawnPoint;
    [SerializeField] private Transform endSpawnPoint;
    [SerializeField] private Transform alternativeSpawnPoint;

    public SectionType SectionType => sectionType;
    public bool HasEndPath => hasEndPath;
    public bool HasAlternativePath => hasAlternativePath;

    private static readonly int StartHash = Animator.StringToHash("Start");
    private static readonly int EndHash = Animator.StringToHash("End");
    private static readonly WaitForSeconds _waitForSeconds1 = new(1f);

    private LevelSection previousSection;
    private LevelSection nextSection;
    private LevelSection alternativeSection;
    private GameSettingsSO gameSettings;
    private Animator levelLoaderAnimator;

    private bool isAlternativeSection = false;

    private readonly Func<LevelSection, Vector3> GetStartPosition = static section => section.startSpawnPoint.position;
    private readonly Func<LevelSection, Vector3> GetEndPosition = section => section.endSpawnPoint.position;
    private readonly Func<LevelSection, Vector3> GetAlternativePosition = section => section.alternativeSpawnPoint.position;

    private void Awake()
    {
        if (!hasEndPath)
        {
            endTrigger.gameObject.SetActive(false);
        }

        if (!hasAlternativePath)
        {
            alternativeTrigger.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        startTrigger.TeleportEvent += TeleportToPreviousSection;

        if (hasEndPath)
        {
            endTrigger.TeleportEvent += TeleportToNextSection;
        }
        else
        {
            endTrigger.gameObject.SetActive(false);
        }

        if (hasAlternativePath)
        {
            alternativeTrigger.TeleportEvent += TeleportToAlternativeSection;
        }
        else
        {
            alternativeTrigger.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        startTrigger.TeleportEvent -= TeleportToNextSection;

        if (hasEndPath)
        {
            endTrigger.TeleportEvent -= TeleportToNextSection;
        }

        if (hasAlternativePath)
        {
            alternativeTrigger.TeleportEvent -= TeleportToAlternativeSection;
        }
    }

    private void TeleportToPreviousSection(GameObject player)
    {
        if (isAlternativeSection)
        {
            StartCoroutine(TeleportTo(previousSection, player, GetAlternativePosition));
        }
        else
        {
            StartCoroutine(TeleportTo(previousSection, player, GetEndPosition));
        }
    }

    private void TeleportToNextSection(GameObject player)
    {
        StartCoroutine(TeleportTo(nextSection, player, GetStartPosition));
    }

    private void TeleportToAlternativeSection(GameObject player)
    {
        StartCoroutine(TeleportTo(alternativeSection, player, GetStartPosition));
    }

    private IEnumerator TeleportTo(LevelSection newSection, GameObject player, Func<LevelSection, Vector3> getPosition)
    {
        // Show Start Transition
        levelLoaderAnimator.SetTrigger(StartHash);

        yield return _waitForSeconds1;

        NavigateTo(newSection, player, getPosition);

        yield return _waitForSeconds1;

        // Show End Transition
        levelLoaderAnimator.SetTrigger(EndHash);
    }

    private void NavigateTo(LevelSection newSection, GameObject player, Func<LevelSection, Vector3> getPosition)
    {
        if (newSection == null)
        {
            SceneManager.LoadSceneAsync(gameSettings.mainSceneName);
        }
        else
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;
            player.transform.position = getPosition(newSection);
            cc.enabled = true;

            newSection.gameObject.SetActive(true);
        }
    }

    public void InitSection(GameObject player)
    {
        NavigateTo(this, player, GetStartPosition);
    }

    public void Setup(GameSettingsSO gameSettings, Animator levelLoaderAnimator)
    {
        this.gameSettings = gameSettings;
        this.levelLoaderAnimator = levelLoaderAnimator;
    }

    public void SetNextSection(LevelSection levelSection)
    {
        nextSection = levelSection;
    }

    public void SetPreviousSection(LevelSection levelSection, bool isAlternativeSection)
    {
        previousSection = levelSection;
        this.isAlternativeSection = isAlternativeSection;
    }

    public void SetAlternativeSection(LevelSection levelSection)
    {
        alternativeSection = levelSection;
    }
}
