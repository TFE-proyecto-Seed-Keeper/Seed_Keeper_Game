using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AtackSystem : MonoBehaviour, IAttacks
{
    [SerializeField]
    int enemyIndex = 0;

    public EnemySystem enemySelected;

    public float overlapRadius;

    public InputActionReference nextEnemy;

    public List<EnemySystem> enemyList = new List<EnemySystem>();

    [SerializeField]
    float meleAttackRadius, melleAttackDistance, meleeAttackDamage;

    [SerializeField]
    float rangeAtackDamage;
    
    [SerializeField]
    float areaAttackDamage;

    [SerializeField]
    Transform projectileCaster;

    void OnEnable()
    {
        nextEnemy.action.Enable();
        nextEnemy.action.performed += ctx => SelectEnemy();
    }

    void OnDisable()
    {
        nextEnemy.action.Disable();
        nextEnemy.action.performed -= ctx => SelectEnemy();
    }

    public void SelectEnemy()
    {
        Debug.Log("Select Enemy");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius);

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
                enemyList.Add(hitColliders[i].GetComponent<EnemySystem>());
            }
        }

        if (enemyList.Count == 0)
        {
            Debug.Log("No enemy founded");
            return;
        }

        Debug.Log("Enemy founded "+ enemyList.Count);

        if(enemyIndex >= enemyList.Count)
        {
            enemyIndex = 0;
        }

        foreach (var enemy in enemyList)
        {     
            enemy.RelaseEnemy();        
        }

        enemyList[enemyIndex].SetActiveEnemy();
        Debug.Log("Enemy Select "+ enemyList[enemyIndex].name);
        enemySelected = enemyList[enemyIndex];
        transform.LookAt(enemySelected.transform);
        enemyIndex++;

       
    }

    public void SetMeleeAttack()
    {
        print("Send Melee Atack");

        //Collider[] hitColliders = Physics.OverlapSphere(transform.forward + new Vector3(0,0,melleAttackDistance), meleAttackRadius);
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
               //projectilePulling.launchProjectile(projectileCaster.transform projectileCaster.forward * 10f, projectileCaster, ProjectilePulling.ProjectileType.Player);
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
            transform.LookAt(enemySelected.transform);
        
    }
}
