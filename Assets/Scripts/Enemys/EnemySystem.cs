using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class EnemySystem : MonoBehaviour, IDamage, EnemyIO
{
    [SerializeField]
    GameObject indicator;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Slider sliderHealt;

    [SerializeField]
    ParticleSystem deadParticles, atatck_1_particles;

    [SerializeField]
    GameObject Model3D;

    [SerializeField]
    float healt;

    float initialHealt;
    public bool isalive = true;

    [SerializeField]
    GameObject instanceObject;

    [SerializeField]
    float damageAmount, damageRange;

    public UnityEvent EnemydDead;

    private void OnEnable()
    {
        initialHealt = healt;
        sliderHealt.maxValue = healt;
        sliderHealt.value = healt;
    }


   
    
    public void SetAttack_1()
    {
       animator.SetTrigger("attack_1");
    }

    public void InvokeAttack1()
    {
        atatck_1_particles.Play();

        Physics.OverlapSphere(atatck_1_particles.transform.position, damageRange).ToList().ForEach(x =>
        {
            if (x.CompareTag("Player") && x.TryGetComponent<IDamage>(out IDamage damage))
            {
                damage.ReceiveDamage(damageAmount);
            }
        });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(atatck_1_particles.transform.position, damageRange);
        Gizmos.DrawSphere(atatck_1_particles.transform.position, damageRange);
    }
   

    public void ReceiveDamage(float damage)
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
            StartCoroutine(SetDead());
        }
    }

    IEnumerator SetDead()
    {
        isalive = false;
        
        if (FindAnyObjectByType<AtackSystem>() is AtackSystem atackSystem)
        {
            atackSystem.SelectNextEnemy(1);
        }

        yield return new WaitForSeconds(2f);
        deadParticles.Play();
        yield return new WaitForSeconds(0.15f);
        Model3D.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        EnemydDead.Invoke();
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
       
    }

    public void SelectEnemy(GameObject interactor)
    {
        indicator.SetActive(true);
    }

    public void RelaseEnemy(GameObject interactor)
    {
        indicator.SetActive(false);
    }

    public bool IsAlive()
    {
        return isalive;
    }
}
