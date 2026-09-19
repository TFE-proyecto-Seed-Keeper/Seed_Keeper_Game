using UnityEngine;
using UnityEngine.Events;

public class DialogInteractor : MonoBehaviour
{
    public DialogData dialogData;
    public UnityEvent OnDialogEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the dialog trigger.");
            DialogsManager.instance.SetInteractorState(true);

            DialogsManager.instance.SetDialogData(dialogData);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the dialog trigger.");
            DialogsManager.instance.SetInteractorState(false);
            DialogsManager.instance.SetDialogData();
        }   
    }


}

        
    

