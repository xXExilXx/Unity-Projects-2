using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;

public class PlayerListManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI currentPlayersText;
    public TextMeshProUGUI joinLeaveText;

    private List<string> playersList = new List<string>();
    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            UpdateCurrentPlayersText();
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UpdateJoinLeaveText("You joined the room.");
        AddPlayer(PhotonNetwork.LocalPlayer);
        UpdateCurrentPlayersText();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        playersList.Clear();
        currentPlayersText.text = "Current Players:\n";
        joinLeaveText.text = "Join/Leave Messages:\n";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        AddPlayer(newPlayer);
        UpdateJoinLeaveText($"{newPlayer.NickName} joined the room.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        RemovePlayer(otherPlayer);
        UpdateJoinLeaveText($"{otherPlayer.NickName} left the room.");
    }

    private void AddPlayer(Player player)
    {
        if (!playersList.Contains(player.NickName))
        {
            playersList.Add(player.NickName);
            UpdateCurrentPlayersText();
        }
    }

    private void RemovePlayer(Player player)
    {
        if (playersList.Contains(player.NickName))
        {
            playersList.Remove(player.NickName);
            UpdateCurrentPlayersText();
        }
    }

    private void UpdateCurrentPlayersText()
    {
        currentPlayersText.text = "Current Players:\n";

        foreach (string playerName in playersList)
        {
            currentPlayersText.text += $"{playerName}\n";
        }
    }

    private void UpdateJoinLeaveText(string message)
    {
        joinLeaveText.text += $"{message}\n";
    }
}
