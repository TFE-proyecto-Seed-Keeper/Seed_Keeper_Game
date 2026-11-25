using UnityEngine;
using DG.Tweening;

public class FadeInOnEnable : MonoBehaviour
{

    CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            return;

        canvasGroup.DOFade(1f, 1f);
    }
}
