using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject redBlockPrefab;  // Prefab of the red block
    public GameObject blueBlockPrefab;  // Prefab of the blue block
    public Collider spawnBoundary;  // Collider representing the boundary for note spawning
    public float spawnInterval = 1f;  // Interval between note spawns
    public float noteSpeed = 5f;  // Speed of the notes moving towards the player on the X-axis

    private float spawnTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnNote();
            spawnTimer = 0f;
        }
    }

    private void SpawnNote()
    {
        // Calculate random position within the spawn boundary
        Vector3 randomPosition = GetRandomPositionInBoundary();

        // Randomly choose between red and blue block
        GameObject blockPrefab = Random.Range(0, 2) == 0 ? redBlockPrefab : blueBlockPrefab;

        GameObject block = Instantiate(blockPrefab, randomPosition, Quaternion.identity);

        // Customize block properties here

        block.SetActive(true);

        // Add note movement behavior
        NoteMovement noteMovement = block.AddComponent<NoteMovement>();
        noteMovement.SetSpeed(noteSpeed);
    }

    private Vector3 GetRandomPositionInBoundary()
    {
        Vector3 randomPosition = Vector3.zero;

        if (spawnBoundary != null)
        {
            Vector3 boundsMin = spawnBoundary.bounds.min;
            Vector3 boundsMax = spawnBoundary.bounds.max;

            randomPosition.x = Random.Range(boundsMin.x, boundsMax.x);
            randomPosition.y = Random.Range(boundsMin.y, boundsMax.y);
            randomPosition.z = Random.Range(boundsMin.z, boundsMax.z);
        }

        return randomPosition;
    }
}
