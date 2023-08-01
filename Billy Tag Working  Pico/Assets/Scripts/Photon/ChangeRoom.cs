using Photon.Pun;
using Photon.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public string RoomQuene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PhotonNetwork.InRoom)
            {
                StartCoroutine(LeaveAndJoin());
            }
            else
            {
                PhotonVRManager.JoinRandomRoom(RoomQuene, 10); 
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        PhotonNetwork.LeaveRoom();
    }

    IEnumerator LeaveAndJoin()
    {
        PhotonNetwork.LeaveRoom();
        yield return new WaitForSeconds(2);
        PhotonVRManager.JoinRandomRoom(RoomQuene, 10);

    }
}