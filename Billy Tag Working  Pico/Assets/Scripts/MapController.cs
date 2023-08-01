using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject[] mapPartsA; // First set of map parts to toggle
    public GameObject[] mapPartsB; // Second set of map parts to toggle

    private bool playerInsideTrigger;
    private bool cameFromSideA; // Flag to keep track if the player has entered the trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInsideTrigger)
        {
            playerInsideTrigger = true;

            // Toggle the map parts based on the player's entry side
            if (cameFromSideA)
            {
                ToggleMapParts(mapPartsA, false);
                ToggleMapParts(mapPartsB, true);
            }
            else
            {
                ToggleMapParts(mapPartsA, true);
                ToggleMapParts(mapPartsB, false);
            }
            
            // Flip the boolean flag for the next time the player enters the trigger area
            cameFromSideA = !cameFromSideA;
        }
    }

    private void ToggleMapParts(GameObject[] mapParts, bool enable)
    {
        foreach (GameObject mapPart in mapParts)
        {
            mapPart.SetActive(enable);
        }
    }
}
