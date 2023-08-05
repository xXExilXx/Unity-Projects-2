using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using Keyboard;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Button leaveButton;
    public Button joinRandom;
    public Button joinPrivate;
    public TextMeshProUGUI roomCode;
    public TextMeshProUGUI roomCodeSelection;
    public KeyboardManager keyboardManager;
    void Start()
    {
        ConnectToServer();
        leaveButton.onClick.AddListener(Leave);
    }

    void Leave()
    {
        PhotonNetwork.LeaveRoom(gameObject);
    }
    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    void JoinRandomRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;

        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom($"Room {PhotonNetwork.CountOfRooms}", roomOptions, TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server.");
        base.OnConnectedToMaster()
    }

    private void Update()
    {
        roomCodeSelection.text = keyboardManager.GetKeySequence();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        roomCode.text = PhotonNetwork.CurrentRoom.ToString();
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
