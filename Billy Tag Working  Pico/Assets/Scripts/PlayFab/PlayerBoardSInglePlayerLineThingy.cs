using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomButton;
using TMPro;
using Photon.Pun;
using PlayFab;

public class PlayerBoardSInglePlayerLineThingy : MonoBehaviour
{
    public string PlayerName;
    public PhotonView PlayerView;
    public TextMeshProUGUI PlayerNameText;
    public Button ReportButton;
    public Button MuteButton;
    public GameObject ReportReasonButtons;
    
    private string UserId;
    public void Apply()
    {
        PlayerNameText.text = PlayerName;
    }

    void GetUserId()
    {
        //TODO: make logic where script gets user id by play fab using the photonview by the player
    }

    void Update()
    {
        if(ReportButton.ispressed)
        {
            ReportPlayer();
        }
        else if(MuteButton.ispressed)
        {
            MutePlayer();
        }
    }
    void ReportPlayer()
    {
        //TODO: Logic for reporting player
    }

    void MutePlayer()
    {
        //TODO: Logic for evectivly muting a player fopr the client side
    }
}
