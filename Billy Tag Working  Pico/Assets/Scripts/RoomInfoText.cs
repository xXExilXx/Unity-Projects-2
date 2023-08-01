using Photon.Pun;
using TMPro;
using UnityEngine;

public class RoomInfoText : MonoBehaviourPunCallbacks
{
    public TMP_Text roomInfoText;

    private void UpdateRoomInfoText()
    {
        if (PhotonNetwork.InRoom)
        {
            int currentPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            int maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
            string roomName = PhotonNetwork.CurrentRoom.Name;

            roomInfoText.text = $"Room Code: {roomName}\nPlayers: ({currentPlayers}/{maxPlayers})";
        }
        else
        {
            roomInfoText.text = "Not in a room";
        }
    }

    public override void OnJoinedRoom()
    {
        UpdateRoomInfoText();
    }

    public override void OnLeftRoom()
    {
        UpdateRoomInfoText();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdateRoomInfoText();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdateRoomInfoText();
    }
}
