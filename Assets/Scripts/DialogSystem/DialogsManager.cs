
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogsManager : MonoBehaviour
{

    [SerializeField]
    GameObject dialogUI, interactUi;

    public InputActionReference interactionAction;

    public static DialogsManager instance;

    [SerializeField]
    TMPro.TextMeshProUGUI dialogText, nameText;


    [SerializeField]
    AudioClip interactSound, typingSound;

    AudioSource audioSource;

    [SerializeField]
    float typingSpeed;

    DialogData dialogData;

    int dialogIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetInteractorState(bool state)
    {
        interactUi.SetActive(state);
        dialogUI.SetActive(false);
        audioSource.PlayOneShot(interactSound);
        ResetDialogs();
    }

    public void SetDialogData(DialogData data)
    {
        dialogData = data;
    }
    public void SetDialogData()
    {
        dialogData = null;
    }

    public bool GetInteractoState()
    {
        return interactUi.activeSelf;
    }

    public bool GetDialogUiState()
    {
        return dialogUI.activeSelf;
    }

    private void Start()
    {
        interactionAction.action.Enable();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        interactionAction.action.started += OnInteractionPressed;
        interactionAction.action.canceled += OnInteractionReleased;
    }

    private void OnDisable()
    {
        interactionAction.action.started -= OnInteractionPressed;
        interactionAction.action.canceled -= OnInteractionReleased;
    }

    private void OnInteractionPressed(InputAction.CallbackContext context)
    {
        if (GetInteractoState())
        {
            print(" interactuanado.");
           
            dialogUI.SetActive(true);
            interactUi.SetActive(false);
            NextDialogue();

            return;
        }
        else if(GetDialogUiState())
        {
            print("diálogo activo.");
           if (dialogData != null)
            {
                NextDialogue();
                
            }
            return;
        }
        print("No hay un interactor activo para interactuar.");

    }

    private void OnInteractionReleased(InputAction.CallbackContext context)
    {
       
    }

    public void NextDialogue()
    {
        if(dialogAnimation != null)
        {
            StopCoroutine(dialogAnimation);
        }


        if (dialogIndex >= dialogData.dialogLines.Count)
        {
            SetInteractorState(true);
            return;
        }
        else
        {
            nameText.text = dialogData.dialogName;
            dialogAnimation = StartCoroutine(typingEfect(dialogData.dialogLines[dialogIndex]));
            dialogIndex++;
        }
        
       
    }

    Coroutine dialogAnimation;

    IEnumerator typingEfect( string text)
    {
       string temporaltext = "";
        dialogText.text = temporaltext;

        for (int i = 0; i < text.Length; i++)
        {
            if(i%2 == 0)
            {
                audioSource.PlayOneShot(typingSound);
            }
            temporaltext += text[i];
            dialogText.text = temporaltext;

            yield return new WaitForSeconds(typingSpeed/100);
        }
        
    }

    public void ResetDialogs()
    {
        if (dialogAnimation != null)
        {
            StopCoroutine(dialogAnimation);
        }
        dialogText.text = "";
        nameText.text = "";
        dialogIndex = 0;
    }




}
