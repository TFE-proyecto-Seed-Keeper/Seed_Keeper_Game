using UnityEngine;
using UnityEngine.Events;

public class TriggerInteractor3D : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent, OnTriggerStayEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerExitEvent?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerStayEvent?.Invoke();
        }
    }
}
