using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class SliderExtentions
{
    public static IEnumerator AnimateSliderBar(this Slider slider, float target, float duration, Action onComplete)
    {
        float start = slider.value;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(start, target, elapsed / duration);
            yield return null;
        }

        slider.value = target;
        onComplete();
    }
}
