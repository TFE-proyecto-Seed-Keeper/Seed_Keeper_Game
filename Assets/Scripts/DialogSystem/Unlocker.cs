
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static SeedData;

public class Unlocker : MonoBehaviour
{
    [SerializeField]
    private GameObject interactor;

    [SerializeField]
    TMPro.TextMeshProUGUI titleText;

    [SerializeField]
    Image seedImage;

    public InputActionReference interactionAction;

    AudioSource audioSource;

    [SerializeField]
    AudioClip SuccessSound, failSound, clickSound;

    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    GameObject GameobjectBlocker;

    Collider[] collidersToDisable;

    public SeedData.seedType seedAsset;

    [SerializeField]
    int seedCost;

    private void Awake()
    {
        interactor.SetActive(false);
        collidersToDisable = GetComponents<Collider>();
    }

    private void Start()
    {
        interactionAction.action.Enable();
        audioSource = GetComponent<AudioSource>();


    }

    public void SetSeedData()
    {
        var seedSystem = FindAnyObjectByType<SeedSystem>();

        if (seedSystem)
        {
            var data = seedSystem.GetSeedData(seedAsset);
             titleText.text = data.name;
            seedImage.sprite = data.seedSprite;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the dialog trigger.");
            SetSeedData();
            interactor.SetActive(true);
            ActivateAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the dialog trigger.");
            interactor.SetActive(false);
            DisableAction();
        }
    }

    private void ActivateAction()
    {
        interactionAction.action.started += OnInteractionPressed;
        interactionAction.action.canceled += OnInteractionReleased;
    }

    private void DisableAction()
    {
        interactionAction.action.started -= OnInteractionPressed;
        interactionAction.action.canceled -= OnInteractionReleased;
    }

    private void OnInteractionReleased(InputAction.CallbackContext context)
    {
        if (CheckSeeds())
        {
            Unlock();
        }
        else
        {
            CantUnlock();
        }
    }

    private void OnInteractionPressed(InputAction.CallbackContext context)
    {
        audioSource.PlayOneShot(clickSound);
    }


    public void Unlock()
    {
        audioSource.PlayOneShot(SuccessSound);

        GameobjectBlocker.SetActive(false);

        DisableAction();

        foreach (var item in collidersToDisable)
        {
            item.enabled = false;
        }
        particles.Play();

        interactor.SetActive(false);
    }

    public void CantUnlock()
    {
        if (FindAnyObjectByType<MessageSystem>())
        {
            MessageSystem messageSystem = FindAnyObjectByType<MessageSystem>();
            messageSystem.SetMessage("¡Sin semillas!");
            audioSource.PlayOneShot(failSound);
        }
        else 
        { 
          print("MessageSystem not found in the scene.");
        }
    }

    public bool CheckSeeds()
    {
        SeedSystem seedSystem = FindAnyObjectByType<SeedSystem>();
        if (seedSystem != null)
        {
            if (seedSystem.GetSeedTypeCount(seedAsset) >= seedCost)
            {
                seedSystem.RemoveSeedType(seedAsset, seedCost);
                return true;
            }
            else 
            {
                return false;
            }
        }
        else
        {
            Debug.LogWarning("SeedSystem not found in the scene.");
            return false;
        }

    }


}
