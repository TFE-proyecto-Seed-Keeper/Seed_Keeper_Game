using UnityEngine;

public interface EnemyIO 
{
    public void SelectEnemy(GameObject interactor);

    public void RelaseEnemy(GameObject interactor);

    public bool IsAlive();
}
