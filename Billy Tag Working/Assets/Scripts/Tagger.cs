using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Tagger : MonoBehaviourPunCallbacks, IPunObservable
{
    public TagManager manager;
    public Light light;
    public Light idleLight;
    public AudioSource audioSource;
    public AudioClip tagSound;
    [Space]
    public bool isLocal;
    public bool ready;
    public int score;

    private bool isTagged;
    private bool shouldSyncTaggedState;
    public float tagDistance = 5f; // The distance within which other players can be tagged

    private void Start()
    {
        light.enabled = false;
        ready = false;
        score = 0;
    }

    private void Update()
    {
        if (ready && isLocal)
        {
            DetectTaggablePlayer();
        }
    }

    private void DetectTaggablePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, tagDistance))
        {
            if (hit.collider.CompareTag("RemotePlayer"))
            {
                Tagger tagger = hit.collider.GetComponent<Tagger>();
                if (tagger != null && tagger.isTagged)
                {
                    manager.AddTaggedPlayer(this);
                    light.enabled = true;
                    audioSource.PlayOneShot(tagSound);
                }
            }
        }
    }

    public void ResetTagger()
    {
        photonView.RPC("ResetStateRPC", RpcTarget.All);
    }

    [PunRPC]
    private void ResetStateRPC()
    {
        light.enabled = false;
        isTagged = false;
        shouldSyncTaggedState = false;
        if (!isLocal)
        {
            idleLight.enabled = true;
        }
    }

    public void SetTagged(bool value)
    {
        if (isTagged != value)
        {
            photonView.RPC("SetTaggedRPC", RpcTarget.All, value);
        }
    }

    [PunRPC]
    private void SetTaggedRPC(bool value)
    {
        isTagged = value;
        if (isTagged)
        {
            light.enabled = true;
            idleLight.enabled = false;
        }
        else
        {
            light.enabled = false;
            idleLight.enabled = true;
        }
    }

    public void IncrementScore()
    {
        score++;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the shouldSyncTaggedState flag and isTagged state over the network
            stream.SendNext(shouldSyncTaggedState);
            if (shouldSyncTaggedState)
            {
                stream.SendNext(isTagged);
            }
        }
        else
        {
            // Receive the shouldSyncTaggedState flag and isTagged state from the network
            shouldSyncTaggedState = (bool)stream.ReceiveNext();
            if (shouldSyncTaggedState)
            {
                isTagged = (bool)stream.ReceiveNext();
            }
        }
    }
}