using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetQuality : MonoBehaviour
{

    public int qualityIndex;

    public TMPro.FontStyles styleNormal;
    public TMPro.FontStyles styleSelected;

    private void Start()
    {
        
    }
    // Start is called before the first frame update
    public void SetQualityGame()
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        
        SetSelectedStyle();
    }

    public void SetSelectedStyle()
    { 
            var parent = transform.parent;
             var  childs = parent.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

            foreach ( var child in childs )
            {
            child.fontStyle = styleNormal;
            }

        var tmp = GetComponent<TMPro.TextMeshProUGUI>();
        tmp.fontStyle = styleSelected;
    }

 
}
