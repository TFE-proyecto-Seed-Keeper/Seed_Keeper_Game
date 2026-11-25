using UnityEngine;
using UnityEngine.Events;

public class EnablerObjects : MonoBehaviour
{
    public UnityEvent   OnEnableObjects;

    public float timeDelay;

    private void Start()
    {
        Invoke(nameof(EnableObjects), timeDelay);
    }

    void EnableObjects()
    {
        OnEnableObjects.Invoke();
    }
}
