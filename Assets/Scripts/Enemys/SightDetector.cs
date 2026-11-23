using UnityEngine;

public class SightDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] float detectionRange = 6f;
    [SerializeField] float detectionAngle = 145f;
    [SerializeField] bool showGizmos = true;

    public GameObject PerformDetection(GameObject target)
    {
        Vector3 toTarget = target.transform.position - transform.position;

        if (toTarget.magnitude > detectionRange)
        {
            return null;
        }

        if (Vector3.Angle(transform.forward, toTarget) > detectionAngle * 0.5f)
        {
            return null;
        }

        Vector3 eye = transform.position + Vector3.up * 1.5f;
        Vector3 targetPoint = target.transform.position + Vector3.up;

        if (Physics.Linecast(eye, targetPoint, out RaycastHit hit, 0, QueryTriggerInteraction.Ignore)
            && !hit.transform.IsChildOf(target.transform))
        {
            return null;
        }

        if (showGizmos && enabled)
        {
            Debug.DrawLine(transform.position + Vector3.up * detectionRange, target.transform.position, Color.green);
        }

        return target;
    }
}
