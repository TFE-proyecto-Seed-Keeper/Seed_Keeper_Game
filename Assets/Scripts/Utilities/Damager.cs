using UnityEngine;

public class Damager : MonoBehaviour
{

    public int damageAmount = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<HealthSystem>();
            playerHealth.ReceiveDamage(damageAmount);
        }
    }
}
