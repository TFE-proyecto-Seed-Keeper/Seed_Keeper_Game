using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameSettingsSO gameSettings;

    [SerializeField]
    CinemachineBrain cinemachineBrain;

     GameObject player;

    [SerializeField]
    Transform[] playerSpawnPoints;

    [SerializeField]
    CinemachineCamera[] cameras;

   
    public UnityEvent OnSecondLoad;

    [SerializeField]
    bool testMode = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        var controller = player.GetComponent<CharacterController>();
        controller.enabled = false;

        if (gameSettings.firstLoad)
        {
            print("First load");
            gameSettings.firstLoad = false;
            
            player.transform.position = playerSpawnPoints[0].position;
        }
        else
        {
            print("Second load");
            player.transform.position = playerSpawnPoints[1].position;
            OnSecondLoad.Invoke();
        }

        controller.enabled = true;
    }


    private void SetGameSettings()
    {
        if (testMode)
            return;    

        if (gameSettings == null)
        {
            Debug.LogError("GameSettingsSO is not assigned in the GameManager.");
            return;
        }

        if (gameSettings.firstLoad)
        {
            print("First load");
            gameSettings.firstLoad = false;
            player.transform.position = playerSpawnPoints[0].position;
            cameras[1].Priority = 1;
            cameras[0].Priority = 0;
            StartCoroutine(StartAfterDelay());
        }
        else
        {
            print("Second load");
            player.transform.position = playerSpawnPoints[1].position;
            OnSecondLoad.Invoke();
        }
    }

    IEnumerator StartAfterDelay()
    {
        print("Start with Delay before");
        yield return new WaitForSeconds(1.5f);
        print("Start with Delay after");
        cameras[1].Priority = 0;
        cameras[0].Priority = 1;
    }
}
