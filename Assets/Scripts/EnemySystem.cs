using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour, IDamage
{
    [SerializeField]
    GameObject indicator;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Slider sliderHealt;

    [SerializeField]
    ParticleSystem deadParticles;

    [SerializeField]
    GameObject Model3D;

    [SerializeField]
    float healt;

    float initialHealt;

    [SerializeField]
    bool isalive = true;

    [SerializeField]
    bool isDummy;

    [SerializeField]
    GameObject instanceObject;

    public UnityEvent EnemydDead;

    private void OnEnable()
    {
        initialHealt = healt;
        sliderHealt.maxValue = healt;
        sliderHealt.value = healt;
    }


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
        if (!isalive)
            return;

        print("recieve Melee Atack o n Enemy amount "+damage);
        animator.SetTrigger("damage");



        if(GetComponentInChildren<TextParticleEmitter>() is TextParticleEmitter textEmitter)
        {
            textEmitter.EmitDamageText(damage);
        }

        healt -= damage;
        sliderHealt.value = healt;

        if (healt <= 0)
        {
            animator.SetTrigger("die");
            isalive = false;
            StartCoroutine(SetDead());
        }
    }

    IEnumerator SetDead()
    {
        yield return new WaitForSeconds(2f);
        deadParticles.Play();
        yield return new WaitForSeconds(0.15f);
        Model3D.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        EnemydDead.Invoke();
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
       
    }
}
