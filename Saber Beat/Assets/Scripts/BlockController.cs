using UnityEngine;

public class BlockController : MonoBehaviour
{
    private float spawnTime;

    public void SpawnAtTime(float time)
    {
        spawnTime = time;
        // Set the cube as inactive initially
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Activate the cube when the spawn time is reached
        if (Time.time >= spawnTime)
        {
            gameObject.SetActive(true);
        }
    }

    // Implement any other functionality or behavior for the block here
}
