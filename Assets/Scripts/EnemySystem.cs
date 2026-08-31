using UnityEngine;

public class EnemySystem : MonoBehaviour, IDamage
{
    [SerializeField]
    GameObject indicator;

    [SerializeField]
    Animator animator;

    public void SetActiveEnemy()
    {
        indicator.SetActive(true);
    }

     public void RelaseEnemy()
    {
        indicator.SetActive(false);
    }

    public void ReciveDamage(float damage)
    {
        print("recieve Melee Atack o n Enemy amount "+damage);
        animator.SetTrigger("damage");
        if(GetComponentInChildren<TextParticleEmitter>() is TextParticleEmitter textEmitter)
        {
            textEmitter.EmitDamageText(damage);
        }
    }
}
