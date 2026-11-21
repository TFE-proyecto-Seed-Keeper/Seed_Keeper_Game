using UnityEngine;
using DG.Tweening;

public class SpawnText : MonoBehaviour
{

   
    public void SetText(string text, Color color, float animationTime, float animationMove)
    {
        var textComponent = GetComponent<TMPro.TextMeshPro>();

        textComponent.text = text;
        textComponent.color = color;
        gameObject.SetActive(true);

        transform.localPosition = Vector3.zero;

        textComponent.DOFade(0, animationTime).SetEase(Ease.InCubic);

        transform.DOLocalMoveY(animationMove, animationTime).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

}
