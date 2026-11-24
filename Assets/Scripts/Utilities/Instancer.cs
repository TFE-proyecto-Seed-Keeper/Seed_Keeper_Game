using UnityEngine;

public class Instancer : MonoBehaviour
{
    public GameObject prefabToPlayerPosition, prefabTosamePosition;

    public void InstanceOnPLayer()
    {        
        if(prefabToPlayerPosition != null)
        {
            InstancerManager._instanceManager.InstancePrefabToPlayerPosition(prefabToPlayerPosition);
        }   
    }

    public void InstanceOnSite()
    {
        if(prefabTosamePosition != null)
        {
            InstancerManager._instanceManager.InstancePrefabTosamePosition(prefabTosamePosition, gameObject);
        }
    }
}
