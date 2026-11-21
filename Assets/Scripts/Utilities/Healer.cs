using UnityEngine;

public class Healer : MonoBehaviour
{
    public int healAmount = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<HealthSystem>();
            playerHealth.Heal(healAmount);
        }
    }
}
