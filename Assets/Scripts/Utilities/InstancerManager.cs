using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

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
    public GameObject InstancePrefabToPlayerPosition(GameObject prefabToInstantiate, GameObject whoInstance)
    {

        var player = GameObject.FindGameObjectWithTag("Player");

        if (prefabToInstantiate != null)
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
        else
        {
            return null;
        }
    }

    public GameObject InstancePrefabTosamePosition(GameObject prefabToInstantiate, GameObject whoInstance)
    {

        var player = GameObject.FindGameObjectWithTag("Player");

        if (prefabToInstantiate != null)
        {
            audioSource.Play();

            var obj = Instantiate(prefabToInstantiate, player.transform.position, Quaternion.identity);

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
        else
        {
            return null;
        }
    }


}
