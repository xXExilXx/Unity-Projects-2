using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.VR;

public class ServerSwitcher : MonoBehaviourPunCallbacks
{
    public List<string> appids = new List<string>();
    public List<string> voiceids = new List<string>();

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        if(returnCode == 32757)
        {
            int random = Random.Range(0, appids.Count);
            PhotonVRManager.ChangeServers(appids[random], voiceids[random]);
        }
    }
}