using System.Collections;
using UnityEngine;

public class AutoStart : MonoBehaviour
{

    public float DelayTime = 0f;

    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartAfterDelay());
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(DelayTime);
        GetComponent<Collider>().enabled = true;
    }


}
