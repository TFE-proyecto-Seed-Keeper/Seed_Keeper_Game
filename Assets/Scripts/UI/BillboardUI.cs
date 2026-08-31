using UnityEngine;

using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool lockYAxis = true; // Set true for ground sprites (trees), false for UI

    private Transform mainCameraTransform;

    void Start()
    {
        // Cache the main camera's transform component for optimal performance
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    // LateUpdate runs after standard Update, ensuring camera movement is finished
    void LateUpdate()
    {
        if (mainCameraTransform == null) return;

        if (lockYAxis)
        {
            // Keep the object upright, rotating it only around the Y-axis
            Vector3 targetPosition = transform.position + mainCameraTransform.forward;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
        else
        {
            // Full billboarding: Match the camera's rotation exactly to prevent edge warping
            transform.rotation = mainCameraTransform.rotation;
        }
    }
}
