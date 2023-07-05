using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    void Update()
    {
        
    }
    private void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecteting...");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server");
        RoomOptions roomoptions = new RoomOptions();
        roomoptions.MaxPlayers = 10;
        roomoptions.IsVisible = true;
        roomoptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Room1",roomoptions,TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        base.OnJoinedRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A Player has joined");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
