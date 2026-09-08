using System;
using UnityEngine;

public abstract class PlayerAttributeSO : ScriptableObject
{
    public event Action<float, float> OnChange;

    protected void TriggerEvent(float currentValue, float maxValue) => OnChange?.Invoke(currentValue, maxValue);
}
