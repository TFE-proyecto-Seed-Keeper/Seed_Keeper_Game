using UnityEngine;
using DG.Tweening;


public class GameOverScreen : MonoBehaviour
{

    CanvasGroup canvasGroup;

    [ SerializeField] private float fadeDuration = 3f;
    [SerializeField] private float showDelay = 3f;

    public void ShowScreen()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        Invoke("AnimateScreen", showDelay);
    }

    void AnimateScreen()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, fadeDuration).SetEase(Ease.InOutSine);
    }
}
