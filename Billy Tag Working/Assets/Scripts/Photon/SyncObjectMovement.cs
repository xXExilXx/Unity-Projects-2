using UnityEngine;
using Photon.Pun;

public class SyncObjectMovement : MonoBehaviourPun, IPunObservable
{
    private Vector3 syncPosition;
    private Quaternion syncRotation;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            // Update the object's position and rotation based on synced data
            transform.position = Vector3.Lerp(transform.position, syncPosition, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRotation, Time.deltaTime * 10f);
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Gather current position and rotation data
            syncPosition = rb.position;
            syncRotation = rb.rotation;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send data to other players
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
        }
        else
        {
            // Receive data from the owner player
            syncPosition = (Vector3)stream.ReceiveNext();
            syncRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
