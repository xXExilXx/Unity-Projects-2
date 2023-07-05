using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInput = null;
    [SerializeField] GameObject RoomPanel;
    [SerializeField] GameObject LobbyPanel;
    [SerializeField] GameObject NonMasterLobbyPanel;
    [SerializeField] TMP_Text statustext;
    [SerializeField] TextMeshProUGUI playerListTextNoHost;
    [SerializeField] TextMeshProUGUI playerListText;
   
    void Start()
    {
        ConnectToMaster();
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        LobbyPanel.SetActive(false);
        NonMasterLobbyPanel.SetActive(false);
    }

    void ConnectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
        statustext.text = "Connecting to Main Server";
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");
        statustext.text = "Connected to Photon Master Server!";
        RoomPanel.SetActive(true);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        statustext.text = "Joining Random Room";
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public void JoinPrivateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 200; // Set maximum number of players in the room
        roomOptions.IsVisible = false; // Hide the room from the lobby
        roomOptions.IsOpen = true; // Allow players to join the room

        string roomName = roomNameInput.text;
        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogError("Room name cannot be empty!");
            statustext.text = "Room name cannot be empty!";
            return;
        }

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        PhotonNetwork.LocalPlayer.NickName = "Player";
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room, creating new one: " + message);
        statustext.text = "No Public Rooms avalible, creating new Room";

        CreateNewRoom();
    }

    void CreateNewRoom()
    {
        statustext.text = "Creating new Room";
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 200; // Set maximum number of players in the room
        roomOptions.IsVisible = true; // Show the room in the lobby
        roomOptions.IsOpen = true; // Allow players to join the room
        PhotonNetwork.AutomaticallySyncScene = true;


        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        statustext.text = "Joined room: " + PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            RoomPanel.SetActive(false);
            NonMasterLobbyPanel.SetActive(true);
        }
        else
        {
            RoomPanel.SetActive(false);
            LobbyPanel.SetActive(true);
        }
        RefreshPlayerList();
        RefreshPlayerList2();

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RefreshPlayerList();
        RefreshPlayerList2();
        statustext.text = "Player " + newPlayer + " Joined!";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RefreshPlayerList();
        RefreshPlayerList2();
        statustext.text = "Player " + otherPlayer + " Left!";
    }

    public void RefreshPlayerList2()
    {
        Player[] players = PhotonNetwork.PlayerList;
        string playerList = "";

        for (int i = 0; i < players.Length; i++)
        {
            playerList += players[i].NickName + "\n";
        }

        playerListText.text = playerList;

        if (players.Length > 5)
        {
            playerListText.fontSize = 18;
        }
        else
        {
            playerListText.fontSize = 24;
        }
    }
    public void RefreshPlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;
        string playerList = "";

        for (int i = 0; i < players.Length; i++)
        {
            playerList += players[i].NickName + "\n";
        }

        playerListTextNoHost.text = playerList;

        if (players.Length > 5)
        {
            playerListTextNoHost.fontSize = 18;
        }
        else
        {
            playerListTextNoHost.fontSize = 24;
        }
    }
}