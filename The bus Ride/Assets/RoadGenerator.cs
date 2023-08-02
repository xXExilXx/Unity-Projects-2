using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public List<GameObject> roadParts;
    public int roadLength = 10; // Number of road parts to spawn initially
    public float roadPartLength = 10f; // Length of each road part
    public float moveSpeed = 5f; // Speed at which the road moves

    private List<GameObject> spawnedRoadParts = new List<GameObject>();

    void Start()
    {
        // Spawn initial road parts
        for (int i = 0; i < roadLength; i++)
        {
            SpawnRoadPart();
        }
    }

    void Update()
    {
        // Move the spawned road parts backward
        foreach (GameObject roadPart in spawnedRoadParts)
        {
            roadPart.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        // Check if the first road part has moved out of view, then spawn a new one and remove the old one
        if (spawnedRoadParts.Count > 0 && spawnedRoadParts[0].transform.position.z < -roadPartLength)
        {
            Destroy(spawnedRoadParts[0]);
            spawnedRoadParts.RemoveAt(0);
            SpawnRoadPart();
        }
    }

    void SpawnRoadPart()
    {
        int randomIndex = Random.Range(0, roadParts.Count);
        Vector3 spawnPosition = Vector3.zero;
        if (spawnedRoadParts.Count > 0)
            spawnPosition = spawnedRoadParts[spawnedRoadParts.Count - 1].transform.position + Vector3.forward * roadPartLength;
        GameObject newRoadPart = Instantiate(roadParts[randomIndex], spawnPosition, Quaternion.identity);
        spawnedRoadParts.Add(newRoadPart);
    }
}
