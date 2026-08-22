using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [Tooltip("The amount of enemies to spawn should be less or equals to the amount of spawn points")]
    [SerializeField] private int enemiesToSpawn = 1;

    private void Awake()
    {
        if(enemiesToSpawn > spawnPoints.Length)
        {
            DisableCollider();
            Debug.LogError($"{nameof(EnemySpawner)}: The amount of enemies to spawn should not be greater than the amout of spawn point");
            return;
        }

        if (enemiesToSpawn == 0 || spawnPoints?.Length == 0 || enemyPrefabs?.Length == 0)
        {
            DisableCollider();
            Debug.Log($"{nameof(EnemySpawner)}: Nothing to spawn");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableCollider();
            SpawnEnemies();
        }
    }

    private void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void SpawnEnemies()
    {
        Transform[] points = spawnPoints.GetUniqueRandomItems(enemiesToSpawn);
        GameObject[] enemies = enemyPrefabs.GetRandomItems(enemiesToSpawn);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemies[i], points[i].position, Quaternion.identity);
        }
    }
}
