using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using LocalPlayerFollower;
using Photon.Voice.PUN;

public class NetworkedPlayer : MonoBehaviour
{
    public Transform PlayerHead;
    public Transform PlayerBody;
    public Transform PlayerLeftHand;
    public Transform PlayerRightHand;
    public GameObject VoiceIndicator;
    private PhotonVoiceView VoiceView;
    private PhotonView photonView;
    void Start()
    {
        VoiceView = GetComponent<PhotonVoiceView>();
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            PlayerBody.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHead.position = LocalPlayer.instance.Head.position;
        PlayerLeftHand.position = LocalPlayer.instance.LeftHand.position;
        PlayerRightHand.position = LocalPlayer.instance.RightHand.position;
        if(VoiceView.IsSpeaking)
        {
            VoiceIndicator.SetActive(true);
        }
        else
        {
            VoiceIndicator.SetActive(false);
        }
    }

}
