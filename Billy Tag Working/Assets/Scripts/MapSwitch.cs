using System.Collections.Generic;
using UnityEngine;

public class MapSwitch : MonoBehaviour
{
    public List<GameObject> lastMaps;
    public List<GameObject> newMaps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject lastMap in lastMaps)
            {
                lastMap.SetActive(false);
            }
            foreach (GameObject newMap in newMaps)
            {
                newMap.SetActive(true);
            }
        }
    }
}