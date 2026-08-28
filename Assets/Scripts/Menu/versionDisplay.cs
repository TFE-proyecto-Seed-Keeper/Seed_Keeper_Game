using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class versionDisplay : MonoBehaviour
{

    TMPro.TextMeshProUGUI displayText;

    private void Start()
    {
        displayText = GetComponent<TMPro.TextMeshProUGUI>();

        displayText.text = Application.productName+ "  "+Application.version + "  - " + Application.unityVersion;
    }
}
