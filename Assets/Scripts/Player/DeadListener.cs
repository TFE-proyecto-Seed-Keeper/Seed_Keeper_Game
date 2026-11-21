using System;
using UnityEngine;
using UnityEngine.Events;

public class DeadListener : MonoBehaviour
{
    public UnityEvent OnDead;

    private void OnEnable()
    { 
           HealthSystem.OnDead += OnPlayerDead;
    }

    private void OnPlayerDead(float obj)
    {
        OnDead?.Invoke();
    }

    private void OnDisable()
    {
        HealthSystem.OnDead -= OnPlayerDead;
    }
}       
    

