using UnityEngine;

public class BillboardingUi : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}
