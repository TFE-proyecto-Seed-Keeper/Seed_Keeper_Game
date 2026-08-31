using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    public event UnityAction<GameObject> TeleportEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TeleportEvent?.Invoke(other.gameObject);
        }
    }
}
