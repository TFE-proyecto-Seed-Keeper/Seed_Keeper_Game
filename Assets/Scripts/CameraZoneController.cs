using UnityEngine;
using  Unity.Cinemachine;

public class CameraZoneController : MonoBehaviour
{

    public CinemachineCamera thisCamera;
    public float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetCameras();
            thisCamera.Priority = 1;
            thisCamera.GetComponent<CinemachinePositionComposer>().CameraDistance = distance;
        }
    }

    void ResetCameras()
    {
        var cameras = FindObjectsByType<CinemachineCamera>();
        foreach (var cam in cameras)   
        {
            cam.Priority = 0;
        }
    }
}
