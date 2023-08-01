using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class LineRendererSync : MonoBehaviourPunCallbacks, IPunObservable
{
    public LineRenderer leftLR;
    public LineRenderer rightLR;
    public bool islocal;
    private PhotonView photonView;
    private List<Vector3> leftLRPositions = new List<Vector3>();
    private List<Vector3> rightLRPositions = new List<Vector3>();
    private bool leftLineDeleted = false;
    private bool rightLineDeleted = false;

    void Update()
    {
        if(PhotonNetwork.InRoom)
        {
            if (!photonView.IsMine)
            {
                SyncLineRenderer(leftLR, leftLRPositions, ref leftLineDeleted);
                SyncLineRenderer(rightLR, rightLRPositions, ref rightLineDeleted);
            }
            else
            {
                // Send line renderer count and line deleted status to remote players
                photonView.RPC("SendLineRendererData", RpcTarget.Others, leftLRPositions.Count, rightLRPositions.Count, leftLineDeleted, rightLineDeleted);
            }
        }
    }

    void Awake()
    {
        if (!islocal)
        {
            photonView = GetComponent<PhotonView>();
        }
    }

    public override void OnJoinedRoom()
    {
        if(islocal)
        {
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
    }

    private void SyncLineRenderer(LineRenderer lr, List<Vector3> positions, ref bool lineDeleted)
    {
        if (positions.Count > 0)
        {
            // Add the new positions to the Line Renderer
            lr.positionCount = positions.Count;
            lr.SetPositions(positions.ToArray());

            // Clear the list after synchronization
            positions.Clear();

            // Mark line as not deleted
            lineDeleted = false;
        }
        else if (!lineDeleted)
        {
            // If the line renderer has been deleted on the client side,
            // notify remote players to delete their lines for the respective hand
            if (lr == leftLR)
            {
                photonView.RPC("DeleteLineRenderer", RpcTarget.Others, false);
            }
            else if (lr == rightLR)
            {
                photonView.RPC("DeleteLineRenderer", RpcTarget.Others, true);
            }

            lineDeleted = true;
        }
    }

    [PunRPC]
    private void SendLineRendererData(int leftCount, int rightCount, bool leftDeleted, bool rightDeleted)
    {
        // Receive line renderer data from the local player on remote players
        leftLRPositions.Clear();
        for (int i = 0; i < leftCount; i++)
        {
            leftLRPositions.Add((Vector3)photonView.Controller.CustomProperties["leftLRPos" + i]);
        }

        rightLRPositions.Clear();
        for (int i = 0; i < rightCount; i++)
        {
            rightLRPositions.Add((Vector3)photonView.Controller.CustomProperties["rightLRPos" + i]);
        }

        leftLineDeleted = leftDeleted;
        rightLineDeleted = rightDeleted;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending data to the network (only for the local player)
            stream.SendNext(leftLRPositions.Count);
            for (int i = 0; i < leftLRPositions.Count; i++)
            {
                stream.SendNext(leftLRPositions[i]);
                photonView.Controller.CustomProperties["leftLRPos" + i] = leftLRPositions[i];
            }

            stream.SendNext(rightLRPositions.Count);
            for (int i = 0; i < rightLRPositions.Count; i++)
            {
                stream.SendNext(rightLRPositions[i]);
                photonView.Controller.CustomProperties["rightLRPos" + i] = rightLRPositions[i];
            }

            stream.SendNext(leftLineDeleted);
            stream.SendNext(rightLineDeleted);
        }
        else
        {
            // Receiving data from the network (for remote players)
            int leftPositionCount = (int)stream.ReceiveNext();
            leftLRPositions.Clear();
            for (int i = 0; i < leftPositionCount; i++)
            {
                leftLRPositions.Add((Vector3)stream.ReceiveNext());
            }

            int rightPositionCount = (int)stream.ReceiveNext();
            rightLRPositions.Clear();
            for (int i = 0; i < rightPositionCount; i++)
            {
                rightLRPositions.Add((Vector3)stream.ReceiveNext());
            }

            leftLineDeleted = (bool)stream.ReceiveNext();
            rightLineDeleted = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    private void DeleteLineRenderer(bool isRightHand)
    {
        // Delete the Line Renderer for the respective hand on remote players
        if (isRightHand)
        {
            rightLR.positionCount = 0;
            rightLRPositions.Clear();
        }
        else
        {
            leftLR.positionCount = 0;
            leftLRPositions.Clear();
        }
    }
}