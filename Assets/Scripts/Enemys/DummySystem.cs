using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DummySystem : MonoBehaviour, IDamage, EnemyIO
{
    private static readonly int DieHash = Animator.StringToHash("die");
    private static readonly int DamageHash = Animator.StringToHash("damage");
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
    public bool isalive = true;

    public bool dropItem = true;

    [SerializeField]
    GameObject instanceObject;

    public UnityEvent EnemydDead, EnemydFirtsDead;

    private void OnEnable()
    {
        initialHealt = healt;
        sliderHealt.maxValue = healt;
        sliderHealt.value = healt;
    }
   

    public void ReceiveDamage(float damage)
    {
        if (!isalive)
            return;

        print("recieve Melee Attack o n Enemy amount "+damage);
        animator.SetTrigger(DamageHash);

        if(GetComponentInChildren<TextParticleEmitter>() is TextParticleEmitter textEmitter)
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

        yield return new WaitForSeconds(2f);
        deadParticles.Play();
        yield return new WaitForSeconds(0.15f);
        Model3D.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        EnemydDead.Invoke();
        if (dropItem)
        {
            EnemydFirtsDead.Invoke();
            dropItem = false;
        }
        
        yield return new WaitForSeconds(0.15f);
        ResetDummy();
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

    void ResetDummy()
    {
        healt = initialHealt;
        sliderHealt.maxValue = healt;
        sliderHealt.value = healt;
        Model3D.SetActive(true);
        animator.SetTrigger("up");
        isalive = true;
    }
}
