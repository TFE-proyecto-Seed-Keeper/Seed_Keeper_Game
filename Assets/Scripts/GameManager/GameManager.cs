using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameSettingsSO gameSettings;

    [SerializeField]
    CinemachineBrain cinemachineBrain;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Transform[] playerSpawnPoints;

    [SerializeField]
    CinemachineCamera[] cameras;

    [SerializeField]
    bool testMode = false;

    private void Awake()
    {
        SetGameSettings();

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
            gameSettings.firstLoad = false;
            player.transform.position = playerSpawnPoints[0].position;
            cameras[1].Priority = 1;
            StartCoroutine(StartAfterDelay());
        }
        else
        {
            player.transform.position = playerSpawnPoints[1].position;
        }
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        cameras[1].Priority = 0;
        cameras[0].Priority = 1;
    }
}
