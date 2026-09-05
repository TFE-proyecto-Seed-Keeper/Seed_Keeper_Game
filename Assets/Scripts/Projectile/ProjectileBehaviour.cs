using UnityEngine;
using UnityEngine.UIElements;

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
    float projectileDelay;

    [SerializeField]
    bool state = false;

    Rigidbody rb;

    public ProjectilePulling.ProjectileType projectileType;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Setprojectile(Vector3 target, Transform origin )
    {
        this.target = target;
        this.origin = origin;
        Invoke("LaunhProjectile", projectileDelay);
    } 
    
    void LaunhProjectile()
    {
        transform.position = origin.position;
        gameObject.SetActive(true);
        state = true;
    }

    void OnEnable()
    {
       Invoke("ResetProjectile", projectileTimeLife);
    }

    void FixedUpdate()
    {
        if (!state)
            return;

        if (target != null)
        {
            Vector3 targetPosition = target + new Vector3(0, 0.5f, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, projectileSpeed * Time.deltaTime);
        }
    }

    public void ResetProjectile()
    {
       if(!state)
       return;
       
       gameObject.SetActive(false);
       transform.position = Vector3.zero;
       state = false;
       target = Vector3.zero;

       print("Projectile desabilitado");

    }

    void OnCollisionEnter(Collision collision)
    {
        print("Projectile Impacto!!!");
        ResetProjectile();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    

}
