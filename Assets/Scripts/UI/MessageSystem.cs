using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private float yAnimationMoveValue, animationTime;
    private Color textColor;
    
    [SerializeField] private Color textsColor;

    [SerializeField]
    List<TextMeshProUGUI> messageTexts = new List<TextMeshProUGUI>();
    
    AudioSource  audioSource;

    

    private void Start()
    {
        messageTexts = new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>(true));

        audioSource = GetComponent<AudioSource>();

        foreach (var text in messageTexts)
        {
            text.gameObject.SetActive(false);
        }

    }

    public void SetMessage(string message)
    {
        audioSource.Play();
        
        for (int i = 0; i < messageTexts.Count; i++)
        {
            if (!messageTexts[i].gameObject.activeInHierarchy)
            {
                AnimateMessage(message, messageTexts[i]);
                return;
            }
        }
    }
    public void AnimateMessage(string message,TextMeshProUGUI messageText )
    {
        //DOTween.Kill(messageText.transform);
        messageText.gameObject.SetActive(false);
        textsColor.a = 1;
        messageText.color = textsColor;
       
        messageText.transform.localPosition = Vector3.zero;
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        messageText.DOFade(0, animationTime);
        messageText.transform.DOLocalMoveY(yAnimationMoveValue, animationTime).onComplete += () =>
        {
            messageText.gameObject.SetActive(false);
        };
    
   
    }

   
}
