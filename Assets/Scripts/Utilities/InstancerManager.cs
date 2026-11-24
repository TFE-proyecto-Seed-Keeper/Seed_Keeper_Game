using DG.Tweening;
using UnityEngine;

public class InstancerManager : MonoBehaviour
{
    static public InstancerManager _instanceManager;

    AudioSource audioSource;

    private void Awake()
    {
        if (_instanceManager == null)
        {
            _instanceManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public GameObject InstancePrefabToPlayerPosition(GameObject prefabToInstantiate)
    {
        if (prefabToInstantiate == null)
        {
            return null;
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        GameObject obj = InstantiatePrefab(prefabToInstantiate, player, player);

        return obj;
    }

    public GameObject InstancePrefabTosamePosition(GameObject prefabToInstantiate, GameObject whoInstance)
    {
        if (prefabToInstantiate == null)
        {
            return null;
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        GameObject obj = InstantiatePrefab(prefabToInstantiate, whoInstance, player);

        return obj;
    }

    private GameObject InstantiatePrefab(GameObject prefabToInstantiate, GameObject whoInstance, GameObject player)
    {
        audioSource.Play();

        var obj = Instantiate(prefabToInstantiate, whoInstance.transform.position, Quaternion.identity);

        var seedAsset = obj.GetComponent<SeedAsset>();
        seedAsset.active = false;

        obj.transform.DOMoveY(obj.transform.position.y + 1.5f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            obj.transform.DOMove(player.transform.position, 0.5f).OnComplete(() =>
            {
                obj.transform.DOMoveY(obj.transform.position.y + 0.5f, 0.1f);
            });
            seedAsset.active = true;
        });
        return obj;
    }
}
