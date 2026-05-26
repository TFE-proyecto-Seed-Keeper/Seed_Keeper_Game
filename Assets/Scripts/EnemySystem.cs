using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField]
    GameObject indicator;

    public void SetActiveEnemy()
    {
        indicator.SetActive(true);
    }

     public void RelaseEnemy()
    {
        indicator.SetActive(false);
    }

}
