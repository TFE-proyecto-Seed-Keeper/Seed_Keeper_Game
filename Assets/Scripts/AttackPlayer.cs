using UnityEngine;

using System.Collections.Generic;

using UnityEngine.InputSystem;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField]
    int enemyIndex = 0;

    public EnemySystem enemySelected;

    public float overlapRadius;

    public InputActionReference nextEnemy;

    public List<EnemySystem> enemyList = new List<EnemySystem>();

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
        enemyIndex++;

       
    }

    
   
}
