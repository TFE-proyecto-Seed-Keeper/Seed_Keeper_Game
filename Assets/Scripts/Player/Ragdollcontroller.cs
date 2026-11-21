using UnityEngine;

public class Ragdollcontroller : MonoBehaviour
{

    private void Start()
    {
        DisableRagDoll();
    }
    public void ActivateRagdoll()
    {
        var ragdoll = GetComponentInChildren<Rigidbody>();
        if (ragdoll != null)
        {
            ragdoll.isKinematic = false;
            ragdoll.detectCollisions = true;
        }
    }

    public void DisableRagDoll()
    {
        var ragdoll = GetComponentInChildren<Rigidbody>();
        if (ragdoll != null)
        {
            ragdoll.isKinematic = true;
            ragdoll.detectCollisions = false;
        }
    }

    
    
}
