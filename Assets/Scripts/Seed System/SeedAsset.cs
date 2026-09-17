using UnityEngine;

public class SeedAsset : MonoBehaviour
{

    public int seedValue;
    public SeedData.seedType seedType;

    public bool active;


    private void OnEnable()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("idle");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!active) return;

        
        Animator animator = GetComponent<Animator>();


        if (other.CompareTag("Player"))
        {

            animator.SetTrigger("get");

            SeedSystem seedSystem = FindAnyObjectByType<SeedSystem>();
            if (seedSystem != null)
            {
                active = false; 
                seedSystem.AddSeedType(seedType, seedValue); // 
            }
            
        }
    }
}
