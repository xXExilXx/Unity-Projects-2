using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruits; // Array of fruit prefabs to spawn
    public Transform spawnArea; // Transform defining the spawn area
    public float spawnRateMin = 1f; // Minimum time between spawns
    public float spawnRateMax = 2f; // Maximum time between spawns

    private void Start()
    {
        // Start spawning fruits
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            // Generate a random spawn position within the spawn area
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2f, spawnArea.position.x + spawnArea.localScale.x / 2f),
                Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2f, spawnArea.position.y + spawnArea.localScale.y / 2f),
                spawnArea.position.z
            );

            // Choose a random fruit from the array
            GameObject randomFruit = fruits[Random.Range(0, fruits.Length)];

            // Instantiate the fruit at the spawn position
            GameObject spawnedFruit = Instantiate(randomFruit, spawnPosition, Quaternion.identity);

            // Set initial velocity for the spawned fruit (e.g., falling down)
            Rigidbody fruitRigidbody = spawnedFruit.GetComponent<Rigidbody>();
            fruitRigidbody.velocity = Vector3.up * Random.Range(2f, 5f);

            // Set the lifespan of the fruit before it gets removed
            float lifespan = Random.Range(2f, 4f);
            Destroy(spawnedFruit, lifespan);

            // Wait for the next spawn
            float spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
