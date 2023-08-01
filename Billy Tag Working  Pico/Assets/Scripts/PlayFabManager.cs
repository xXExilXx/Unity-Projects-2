using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.VR;

public class PlayFabManager : MonoBehaviourPunCallbacks
{

    private string LocalPlayFabID;

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");

        PlayFabPlayerLoggedIn();

        string pUsername = PhotonNetwork.NickName;

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = pUsername
        }, delegate (UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("Display Name Changed!");
        }, delegate (PlayFabError error)
        {
            Debug.Log("Error");
            Debug.Log(error.ErrorDetails);
        });

        LocalPlayFabID = result.PlayFabId;

    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        if (error.Error == PlayFabErrorCode.AccountBanned)
        {
            Debug.Log("PLAYER IS BANNED");
            PhotonNetwork.Disconnect();
        }
        Debug.Log(error.GenerateErrorReport());
    }

    public virtual void PlayFabPlayerLoggedIn()
    {
        PhotonVRManager.Connect();
    }

    public override void OnConnectedToMaster()
    {
        var Hash = PhotonNetwork.LocalPlayer.CustomProperties;
        Hash.Add("PlayFabPlayerID", LocalPlayFabID);
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["PlayFabPlayerID"]);
    }

}