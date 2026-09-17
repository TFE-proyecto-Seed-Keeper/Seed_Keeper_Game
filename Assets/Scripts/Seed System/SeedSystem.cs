using DG.Tweening;

using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SeedSystem : MonoBehaviour
{


    [SerializeField] SeedData seedData;

    Seed selectedSeed;

    [SerializeField] TMP_Text seedNameText;
    [SerializeField] TMP_Text seedValueText;
    [SerializeField] TMP_Text seedAdditionalText;

    [SerializeField] Image seedImage;
    public InputActionReference nextSeedAction, prevSeedAction;

    AudioSource audioSource;

    [SerializeField] AudioClip seedChangeSound, seedAddSound, seedRemoveSound;


    public  void AddSeedType(SeedData.seedType type, int value)
    {
        SelectSeed(type);

        audioSource.PlayOneShot(seedAddSound);

        seedAdditionalText.rectTransform.localPosition = Vector3.zero;

        seedAdditionalText.rectTransform.DOLocalMoveY(10, 1f);

        seedAdditionalText.text = $"+ {value}";

        seedAdditionalText.DOFade(1, 0.5f).OnComplete(() =>
        {
            seedAdditionalText.DOFade(0, 1f);
        });

        selectedSeed.seedcount += value;

        UpdateSeedUI();
    }

    public bool RemoveSeedType(SeedData.seedType type, int value)
    {
        audioSource.PlayOneShot(seedRemoveSound);
        return false;
    }

    public int GetSeedType(SeedData.seedType type)
    {
        return 0;
    }

    public void SelectSeed(SeedData.seedType type)
    {
        print("Select Seed");

        foreach (var seed in seedData.seedList)
        {
            if (seed.seedType == type)
            {
                selectedSeed = seed;
                UpdateSeedUI();
                return;
            }
        }
    }

    private void UpdateSeedUI()
    {
        seedNameText.transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(() =>
        {
            seedNameText.transform.DOScale(Vector3.one, 0.2f);
        });
        seedNameText.text = selectedSeed.name;

        seedValueText.transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(() =>
        {
            seedValueText.transform.DOScale(Vector3.one, 0.2f);
        });
        seedValueText.text = selectedSeed.seedcount.ToString();

        seedImage.transform.DOScale(Vector3.one * 1.2f, 0.2f).OnComplete(() =>
        {
            seedImage.transform.DOScale(Vector3.one, 0.2f);
        });
        seedImage.sprite = selectedSeed.seedDprite;
    }

    void NextSeed(InputAction.CallbackContext context)
    { 
        print("Next Seed");
        var seedIndex = seedData.seedList.IndexOf(selectedSeed);
        int  newIndex = seedIndex >= seedData.seedList.Count-1 ? 0 : seedIndex + 1;
        selectedSeed = seedData.seedList[newIndex];
        UpdateSeedUI();
        audioSource.PlayOneShot(seedChangeSound);

    }

    void PrevSeed(InputAction.CallbackContext context)
    {
        print("Previous Seed");
        var seedIndex = seedData.seedList.IndexOf(selectedSeed);
        int newIndex = seedIndex <= 0 ? seedData.seedList.Count - 1 : seedIndex - 1;
        selectedSeed = seedData.seedList[newIndex];
        UpdateSeedUI();
        audioSource.PlayOneShot(seedChangeSound);
    }




    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        seedAdditionalText.alpha = 0;

        if (seedData != null)
        {
            selectedSeed = seedData.seedList[0];
            UpdateSeedUI();
        }
    }

    private void OnEnable()
    {
        nextSeedAction.action.started += NextSeed;
        prevSeedAction.action.started += PrevSeed;

    }

    private void OnDisable()
    {
        nextSeedAction.action.started -= NextSeed;
        prevSeedAction.action.started -= PrevSeed;
    }

    [ContextMenu("Test Add Seeds")]
    public void TestAddSeeds()
    { 
        AddSeedType(SeedData.seedType.totumo, 5);
    }

}
