using UnityEngine;

public class Ragdollcontroller : MonoBehaviour
{
    private Rigidbody ragdoll;

    private void Start()
    {
        ragdoll = GetComponentInChildren<Rigidbody>();

        DisableRagDoll();
    }
    public void ActivateRagdoll()
    {
        if (ragdoll != null)
        {
            ragdoll.isKinematic = false;
            ragdoll.detectCollisions = true;
        }
    }

    public void DisableRagDoll()
    {
        if (ragdoll != null)
        {
            ragdoll.isKinematic = true;
            ragdoll.detectCollisions = false;
        }
    }
}
