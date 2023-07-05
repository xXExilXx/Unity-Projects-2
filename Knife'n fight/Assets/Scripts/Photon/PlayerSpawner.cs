using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // Check if the current client is already in a room
            if (PhotonNetwork.InRoom)
            {
                // Spawn the player for the local client
                SpawnLocalPlayer();
            }
            else
            {
                // Subscribe to the OnJoinedRoom event
                PhotonNetwork.AddCallbackTarget(this);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        // Spawn the player for the local client
        SpawnLocalPlayer();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Spawn the player for the newly joined player
        SpawnOtherPlayer(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Destroy the player GameObject for the player who left the room
        Destroy(otherPlayer.TagObject as GameObject);
    }

    void SpawnLocalPlayer()
    {
        // Instantiate the player prefab at the current position
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);

        // Assign the player's GameObject as the "TagObject" of the local player
        PhotonNetwork.LocalPlayer.TagObject = player;
    }

    void SpawnOtherPlayer(Player newPlayer)
    {
        // Instantiate the player prefab at a default position
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        // Assign the player's GameObject as the "TagObject" of the new player
        newPlayer.TagObject = player;
    }
}
