using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBoard : MonoBehaviourPunCallbacks
{
    private float CurrentPlayers;
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CurrentPlayers = PhotonNetwork.PlayerList.Length;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CurrentPlayers = PhotonNetwork.PlayerList.Length;
    }

    void Update()
    {

    }
}