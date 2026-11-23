using System.Linq;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] LayerMask detectionMask;
    [SerializeField] float detectionRadius = 6.0f;
    [SerializeField] bool showGizmos = true;

    public GameObject DetectedTarget;

    public GameObject UpdateDetector()
    {
        var target = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask)
            .FirstOrDefault(x => x.CompareTag("Player"));

        DetectedTarget = target == null? null: target.gameObject;

        return DetectedTarget;
    }

    private void OnDrawGizmos()
    {
        if(!showGizmos || !enabled)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
