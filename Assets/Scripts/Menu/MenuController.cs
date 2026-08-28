using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public SetQuality setQuality;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    // Update is called once per frame
    void SetDefaults()
    {
        setQuality.SetQualityGame();
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
