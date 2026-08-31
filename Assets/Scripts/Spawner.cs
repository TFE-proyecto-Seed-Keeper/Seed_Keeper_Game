using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] objectPrefabs;
    [Tooltip("The amount of objects to spawn should be less or equals to the amount of spawn points")]
    [SerializeField] private int objectsToSpawn = 1;

    private void Awake()
    {
        if(objectsToSpawn > spawnPoints.Length)
        {
            DisableCollider();
            Debug.LogError($"{nameof(Spawner)}: The amount of objects to spawn should not be greater than the amout of spawn point");
            return;
        }

        if (objectsToSpawn == 0 || spawnPoints?.Length == 0 || objectPrefabs?.Length == 0)
        {
            DisableCollider();
            Debug.Log($"{nameof(Spawner)}: Nothing to spawn");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableCollider();
            SpawnObjects();
        }
    }

    private void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void SpawnObjects()
    {
        Transform[] points = spawnPoints.GetUniqueRandomItems(objectsToSpawn);
        GameObject[] objects = objectPrefabs.GetRandomItems(objectsToSpawn);

        for (int i = 0; i < objectsToSpawn; i++)
        {
            Instantiate(objects[i], points[i].position, Quaternion.identity);
        }
    }
}
