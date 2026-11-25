using UnityEngine;
using  Unity.Cinemachine;

public class CameraZoneController : MonoBehaviour
{

    public CinemachineCamera inCamera, outcamera;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetCameras();
            if (inCamera != null)
            {
                inCamera.Priority = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetCameras();
            outcamera.Priority = 1;
           
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
