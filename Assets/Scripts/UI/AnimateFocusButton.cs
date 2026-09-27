
using UnityEngine;
using DG.Tweening;

public class AnimateFocusButton : MonoBehaviour
{

    [SerializeField] private float timeAnimation = 0.8f;


    [SerializeField] private float maxScale = 1.1f;

    void Start()
    {

        transform.DOScale(maxScale, timeAnimation)
            .SetEase(Ease.InOutQuad) 
            .SetLoops(-1, LoopType.Yoyo); 
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}
