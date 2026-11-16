using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class OnceInstancer : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the dialog trigger.");

            if (prefabToInstantiate != null)
            {
                audioSource.Play();

                var obj = Instantiate(prefabToInstantiate, other.transform.position, Quaternion.identity);

                var seedAsset = obj.GetComponent<SeedAsset>();
                seedAsset.active = false;

                obj.transform.DOMoveY(obj.transform.position.y + 1.5f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    obj.transform.DOMove(other.transform.position, 0.5f).OnComplete(() =>
                    {
                        obj.transform.DOMoveY(obj.transform.position.y + 0.5f, 0.1f);
                    });
                    seedAsset.active = true;
                });
                    
                

                GetComponent<Collider>().enabled = false; 


            }
        }


           
    }
}
