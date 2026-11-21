
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextsPulling : MonoBehaviour
{
    public Color goodColor, badColor;

    public List<SpawnText> textList;

    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float animationMove = 1f;


    void Awake()
    {
        textList = new List<SpawnText>(GetComponentsInChildren<SpawnText>(true));
        DisableAllText();
    }

    public void ShowText(string text, bool isGood)
    {
        foreach (var item in textList)
        {
            if (!item.gameObject.activeSelf)
            {
                item.SetText(text, isGood ? goodColor : badColor, animationTime, animationMove);
                return;
            }
        }
    }

    void DisableAllText()
    {

        foreach (var item in textList)
        {
            item.gameObject.SetActive(false);
        }

    }
}
