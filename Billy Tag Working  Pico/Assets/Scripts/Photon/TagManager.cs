using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TagManager : MonoBehaviourPunCallbacks
{
    public GorillaLocomotion.Player locomotion;
    public AudioSource endSound;
    [Header("Debug")]
    [Space]
    public bool started;
    public List<Tagger> taggers = new List<Tagger>();
    public int startPlayers;
    public PhotonView photonView;

    private int taggedPlayers;

    private void CheckPlayers()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable();
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                started = true;
                if ($"{hastable["queue"]}" == "Deathmatch")
                {
                    photonView.RPC("StartGame", RpcTarget.AllBuffered);
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        CheckPlayers();
        FindTaggers();
    }

    public override void OnJoinedRoom()
    {
        CheckPlayers();
        FindTaggers();
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        foreach (PhotonView view in photonViews)
        {
            if (view.IsMine)
            {
                photonView = view;
                break;
            }
        }
    }

    [PunRPC]
    private void StartGame()
    {
        startPlayers = PhotonNetwork.PlayerList.Length;

        foreach (Tagger tagger in taggers)
        {
            tagger.ready = true;
        }
    }

    [PunRPC]
    IEnumerator EndGame()
    {
        endSound.Play();
        taggedPlayers = 0;

        foreach (Tagger tagger in taggers)
        {
            tagger.ready = false;
        }

        locomotion.disableMovement = true;
        yield return new WaitForSeconds(5);
        locomotion.disableMovement = false;

        foreach (Tagger tagger in taggers)
        {
            tagger.ResetTagger();
        }

        photonView.RPC("StartGame", RpcTarget.AllBuffered);
    }

    public void AddTaggedPlayer(Tagger tagger)
    {
        taggedPlayers++;
        tagger.IncrementScore();

        if (taggedPlayers == startPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("EndGame", RpcTarget.All);
            }
        }
    }

    private void FindTaggers()
    {
        taggers.Clear();
        Tagger[] foundTaggers = FindObjectsOfType<Tagger>();

        foreach (Tagger tagger in foundTaggers)
        {
            taggers.Add(tagger);
        }
    }
}
