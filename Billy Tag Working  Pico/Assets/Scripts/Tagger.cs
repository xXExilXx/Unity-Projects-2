using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Tagger : MonoBehaviourPunCallbacks, IPunObservable
{
    public TagManager manager;
    public SkinnedMeshRenderer meshRenderer;
    public Material taggedMaterial;
    public Material idleMaterial;
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
        meshRenderer.material = idleMaterial;
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
                    SetTagged(true);
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
        meshRenderer.material = idleMaterial;
        isTagged = false;
        shouldSyncTaggedState = false;
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
            meshRenderer.material = taggedMaterial;
        }
        else
        {
            meshRenderer.material = idleMaterial;
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