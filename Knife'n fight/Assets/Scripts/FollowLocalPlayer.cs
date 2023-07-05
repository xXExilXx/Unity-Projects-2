using UnityEngine;

public class FollowLocalPlayer : MonoBehaviour
{
    public string localPlayerTag = "LocalPlayer";

    private GameObject localPlayer;

    private void Start()
    {
        // Find the LocalPlayer object
        localPlayer = GameObject.FindGameObjectWithTag(localPlayerTag);
        if (localPlayer == null)
        {
            Debug.LogError($"Failed to find object with tag {localPlayerTag}.");
            return;
        }
    }

    private void Update()
    {
        if (localPlayer == null)
        {
            return;
        }

        // Set the position of the current object to match the LocalPlayer's position
        transform.position = localPlayer.transform.position;

    }
}
