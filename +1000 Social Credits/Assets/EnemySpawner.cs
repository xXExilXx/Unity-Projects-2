using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTime = 3f; // time in seconds between each spawn
    public Transform[] spawnPoints; // array of spawn points

    private int enemyCount;
    private GameObject enemyPrefab;

    void Start()
    {
        enemyPrefab = gameObject;
        enemyCount = transform.childCount;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        enemyCount = transform.childCount;

        if (enemyCount <= 1)
        {
            // choose a random spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // instantiate the enemy at the spawn point
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            // choose a random spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // copy the enemy prefab and spawn it in
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            newEnemy.transform.parent = transform;
        }
    }
}
