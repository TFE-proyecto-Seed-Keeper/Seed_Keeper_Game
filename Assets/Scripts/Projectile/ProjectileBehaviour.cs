using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    Vector3 target;

    Transform origin;

    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    float projectileTimeLife;

    [SerializeField]
    float projectileDelay, pojectileDisableDelay;

    [field: SerializeField] public float ProjectileDamage { get;  set;}

    [SerializeField]
    float impactDistance , inpactRadius;

    [SerializeField]
    bool state = false;

    [SerializeField] private ParticleSystem projectileParticles, HitParticles;

    Rigidbody rb;

    public ProjectilePulling.ProjectileType projectileType;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Setprojectile(Vector3 target, Transform origin, float damage )
    {
        ProjectileDamage = damage;
        this.target = target;
        this.origin = origin;
        Invoke("LaunhProjectile", projectileDelay);
    } 
    
    void LaunhProjectile()
    {
        transform.position = origin.position;
        gameObject.SetActive(true);
        projectileParticles.Play();
        HitParticles.Stop();
        state = true;
    }

    void SetDamage(float damage)
    {
        projectileParticles.Stop();
        HitParticles.Play();
        Invoke("ResetProjectile", pojectileDisableDelay);
        Vector3 overlapCenter = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(overlapCenter, inpactRadius);

        foreach (var hitCollider in hitColliders)
        {
            ExecuteEvents.Execute<IDamage>(hitCollider.gameObject, null, (handler, eventData) => handler.ReceiveDamage(damage));
        }
    }

    void OnEnable()
    {
       Invoke("ResetProjectile", projectileTimeLife);
    }

    void Update()
    {
       MoveProjectile();
       
    }

    void MoveProjectile()
    {
        if (!state)
            return;

        if (target != null)
        {
            Vector3 targetPosition = target + new Vector3(0, 1f, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, projectileSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPosition) < impactDistance)
            {
                print("Projectile Impacto!!!");
                CancelInvoke("ResetProjectile");
                SetDamage(ProjectileDamage);
                state = false;
            }
        }
      
    }

    public void ResetProjectile()
    {
       gameObject.SetActive(false);
       transform.position = Vector3.zero;
       state = false;
       target = Vector3.zero;

       print("Projectile desabilitado");
    }

   

    

}
