using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class EnemySystem : MonoBehaviour, IDamage, EnemyIO
{
    private static readonly int CanMoveHash = Animator.StringToHash("canMove");
    private static readonly int DieHash = Animator.StringToHash("die");
    private static readonly int HitHash = Animator.StringToHash("hit");

    [SerializeField]
    GameObject indicator;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Slider sliderHealt;

    [SerializeField]
    ParticleSystem deadParticles, attack_1_particles;

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

    public UnityEvent EnemyDead;

    private bool canMove = true;
    private bool canAttack = true;

    private void OnEnable()
    {
        initialHealt = healt;
        sliderHealt.maxValue = healt;
        sliderHealt.value = healt;
    }

    private void Update()
    {
        animator.SetBool(CanMoveHash, canMove);
    }

    public void PerformAttack1()
    {
        if(!isalive)
        {
            return;
        }

        attack_1_particles.Play();

        Physics.OverlapSphere(attack_1_particles.transform.position, damageRange).ToList().ForEach(x =>
        {
            if (x.CompareTag("Player") && x.TryGetComponent(out IDamage damage))
            {
                damage.ReceiveDamage(damageAmount);
            }
        });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attack_1_particles.transform.position, damageRange);
        Gizmos.DrawSphere(attack_1_particles.transform.position, damageRange);
    }

    public void ReceiveDamage(float damage)
    {
        if (!isalive)
            return;

        print("recieve Melee Attack o n Enemy amount "+damage);
        DeactivateMovement();
        animator.SetTrigger(HitHash);

        if (GetComponentInChildren<TextParticleEmitter>() is TextParticleEmitter textEmitter)
        {
            textEmitter.EmitDamageText(damage);
        }

        healt -= damage;
        sliderHealt.value = healt;

        if (healt <= 0)
        {
            animator.SetTrigger(DieHash);
            StartCoroutine(SetDead());
        }
    }

    IEnumerator SetDead()
    {
        isalive = false;
        
        if (FindAnyObjectByType<AtackSystem>() is AtackSystem attackSystem)
        {
            attackSystem.SelectNextEnemy(1);
        }
        
        EnemyDead.Invoke();

        yield return new WaitForSeconds(3f);
        deadParticles.Play();
        yield return new WaitForSeconds(0.15f);
        Model3D.SetActive(false);
        yield return new WaitForSeconds(0.75f);
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

    public bool IsAlive() => isalive;

    public bool CanMove => canMove;

    public bool CanAttack => canAttack;

    public void ActivateMovement()
    {
        canMove = true;
        canAttack = true;
    }

    public void DeactivateMovement()
    {
        canMove = false;
        canAttack = false;
    }
}
