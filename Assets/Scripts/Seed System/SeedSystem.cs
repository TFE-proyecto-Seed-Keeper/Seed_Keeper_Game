using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeedSystem : MonoBehaviour
{


    [SerializeField] SeedData seedData;

    Seed selectedSeed;

    [SerializeField] TMP_Text seedNameText;
    [SerializeField] TMP_Text seedValueText;
    [SerializeField] TMP_Text seedAdditionalText;
    public InputActionReference nextSeedAction, prevSeedAction;


    public  void AddSeedType(SeedData.seedType type, int value)
    {
      
    }

    public bool RemoveSeedType(SeedData.seedType type, int value)
    {
        return false;
    }

    public int GetSeedType(SeedData.seedType type)
    {
        return 0;
    }

    public void SelectSeed()
    {
        selectedSeed = seedData.seedList[0];
    }

    private void UpdateSeedUI()
    {
        seedNameText.text = selectedSeed.name;
        seedValueText.text = selectedSeed.seedcount.ToString();
        
    }

    void NextSeed(InputAction.CallbackContext context)
    { 
        print("Next Seed");

    }

    void PrevSeed(InputAction.CallbackContext context)
    {
        print("Previous Seed");
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

}
