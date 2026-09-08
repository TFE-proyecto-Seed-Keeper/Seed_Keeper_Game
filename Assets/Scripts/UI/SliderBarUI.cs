using UnityEngine;
using UnityEngine.UI;

public class SliderBarUI : MonoBehaviour
{
    [SerializeField] private Slider sliderBar;
    [Tooltip("Total time it should spend for decreasing the 100% of the slider bar")]
    [SerializeField] private float smoothSpeed = 2f;
    [SerializeField] private PlayerAttributeSO attribute;

    private Coroutine animationCoroutine;

    private void OnEnable()
    {
        attribute.OnChange += UpdateSliderBar;
    }

    private void OnDisable()
    {
        attribute.OnChange -= UpdateSliderBar;
    }

    private void UpdateSliderBar(float current, float max)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        float targetValue = current / max;
        float duration = smoothSpeed / max;

        animationCoroutine = StartCoroutine(sliderBar.AnimateSliderBar(targetValue, duration, () => animationCoroutine = null));
    }
}
