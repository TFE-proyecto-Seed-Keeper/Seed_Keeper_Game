using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AtackSystem : MonoBehaviour, IAttacks
{
    [SerializeField]
    int enemyIndex = 0;

    public EnemySystem enemySelected;

    public float enemySearchRadius;

    public InputActionReference nextEnemy;
    public InputActionReference prevEnemy;

    public List<EnemySystem> enemyList = new List<EnemySystem>();

    [SerializeField]
    float meleAttackRadius, melleAttackDistance, meleeAttackDamage;

    [SerializeField]
    float rangeAtackDamage;
    
    [SerializeField]
    float areaAttackDamage, areaAttackRadius, areaAttackvFXDelay, areaAttackCount, areaDelayBetweenAttacks;

    [SerializeField]
    Transform projectileCaster;

    void OnEnable()
    {
        nextEnemy.action.Enable();
        nextEnemy.action.performed += ctx => SelectNextEnemy(1);
        
        prevEnemy.action.Enable();
        prevEnemy.action.performed += ctx => SelectNextEnemy(-1);
    }

    void OnDisable()
    {
        nextEnemy.action.Disable();
        nextEnemy.action.performed -= ctx => SelectNextEnemy(1);
        
        prevEnemy.action.Disable();
        prevEnemy.action.performed -= ctx => SelectNextEnemy(-1);
    }

    public void SelectNextEnemy(int selectDirection)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemySearchRadius);

        if(enemyList.Count > 0)
        {
            foreach (var enemy in enemyList)
            {     
                enemy.RelaseEnemy();        
            }
        }
        else
        {
            enemyIndex = 0;
        }
        enemyList.Clear();

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("enemy"))
            {
                var enemySystem = hitColliders[i].GetComponent<EnemySystem>();
                if (enemySystem)
                {
                    if (enemySystem.isalive)
                    {
                        enemyList.Add(enemySystem);
                    }
                }

               
            }
        }

        if (enemyList.Count == 0)
        {
            Debug.Log("No enemy founded");
            return;
        }

        //Debug.Log("Enemy founded "+ enemyList.Count);
        
        enemyIndex+= selectDirection;

        if(enemyIndex >= enemyList.Count)
        {
            enemyIndex = 0;
        }
        else if (enemyIndex < 0)
        {
            enemyIndex = enemyList.Count-1;
        }

        foreach (var enemy in enemyList)
        {     
            enemy.RelaseEnemy();        
        }

        enemyList[enemyIndex].SetActiveEnemy();
        Debug.Log("Enemy Select index "+enemyIndex+" " +enemyList[enemyIndex].name);
        enemySelected = enemyList[enemyIndex];
        transform.LookAt(enemySelected.transform);
       
    }

    public void RelaseEnemy()
    {
        enemySelected = null;
        SelectNextEnemy(1);
    }

    public void SetMeleeAttack()
    {
        print("Send Melee Atack");

        Vector3 overlapCenter = transform.position + (transform.forward * melleAttackDistance);
        Collider[] hitColliders = Physics.OverlapSphere(overlapCenter, meleAttackRadius);

        foreach (var hitCollider in hitColliders)
        {
            ExecuteEvents.Execute<IDamage>(hitCollider.gameObject, null, (handler, eventData) => handler.ReceiveDamage(meleeAttackDamage));
            
        }
    }

    public void SetRangettack()
    {
        if(FindAnyObjectByType<ProjectilePulling>() is ProjectilePulling projectilePulling)
        {
            print("Send Range Atack");
            if(enemySelected == null)
            {
                if(FindAnyObjectByType<MessageSystem>() is MessageSystem messageSystem)
                    messageSystem.SetMessage("Ningún objetivo seleccionado");
            }
            else
            {
                projectilePulling.launchProjectile(enemySelected.transform.position, projectileCaster, ProjectilePulling.ProjectileType.Player, rangeAtackDamage);
                transform.LookAt(enemySelected.transform);
            }
            
        }
    }

    public void SetAreaAttack()
    {
                print("Send area Atack");
            
                if(enemySelected == null)
                {
                    StartCoroutine(StartVFXareaAttack(transform));
                }
                else
                {
                    transform.LookAt(enemySelected.transform);
                    StartCoroutine(StartVFXareaAttack(enemySelected.transform));
                    
                }
        
    }

    public IEnumerator StartVFXareaAttack(Transform targetforAttack)
    {
        ParticleSystem areaAttackVFX;
        areaAttackVFX = GetComponentInChildren<AttacksVFXcontroller>().AreaAttackVFX;
        
        
        areaAttackVFX.Stop();
        
        areaAttackVFX.transform.position = targetforAttack.position;
        areaAttackVFX.transform.SetParent(null);
        
        yield return new WaitForSeconds(areaAttackvFXDelay);
        
        areaAttackVFX.Play();
        
        yield return new WaitForSeconds(areaDelayBetweenAttacks);

        for (int i = 0; i < areaAttackCount; i++)
        {
            if (targetforAttack)
            {
                SetAreaDamage(areaAttackDamage, targetforAttack.position, areaAttackRadius);
            }

           
            yield return new WaitForSeconds(areaDelayBetweenAttacks);
        }
        
        areaAttackVFX.transform.SetParent(areaAttackVFX.transform);
        areaAttackVFX.transform.localPosition = Vector3.zero;
        
    }

    void SetAreaDamage(float damage,  Vector3 damagePlace, float radius)
    {
       
        Collider[] hitColliders = Physics.OverlapSphere(damagePlace, radius);

        foreach (var hitCollider in hitColliders)
        {
            ExecuteEvents.Execute<IDamage>(hitCollider.gameObject, null, (handler, eventData) => handler.ReceiveDamage(meleeAttackDamage));
            
        }
    }
}
