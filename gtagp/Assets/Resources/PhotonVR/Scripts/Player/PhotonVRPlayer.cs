using System;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using TMPro;

namespace Photon.VR.Player
{
    public class PhotonVRPlayer : MonoBehaviourPun
    {
        [Header("Objects")]
        public Transform Head;
        public Transform LeftHand;
        public Transform RightHand;
        [Tooltip("The objects that will get the colour of the player applied to them")]
        public List<MeshRenderer> ColourObjects;

        [Header("Other")]
        public TextMeshPro NameText;
        public bool HideLocalPlayer = true;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                PhotonVRManager.Manager.LocalPlayer = this;
                if (HideLocalPlayer)
                {
                    Head.gameObject.SetActive(false);
                    RightHand.gameObject.SetActive(false);
                    LeftHand.gameObject.SetActive(false);
                }
            }

            if(NameText != null)
                NameText.text = photonView.Owner.NickName;
            foreach (MeshRenderer renderer in ColourObjects)
            {
                renderer.material.color = JsonUtility.FromJson<Color>((string)photonView.Owner.CustomProperties["Colour"]);
            }

            // It will delete automatically when you leave the room
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                Head.transform.position = PhotonVRManager.Manager.Head.transform.position;
                Head.transform.rotation = PhotonVRManager.Manager.Head.transform.rotation;

                RightHand.transform.position = PhotonVRManager.Manager.RightHand.transform.position;
                RightHand.transform.rotation = PhotonVRManager.Manager.RightHand.transform.rotation;

                LeftHand.transform.position = PhotonVRManager.Manager.LeftHand.transform.position;
                LeftHand.transform.rotation = PhotonVRManager.Manager.LeftHand.transform.rotation;
            }
        }

        public void RefreshPlayerValues() => photonView.RPC("RPCRefreshPlayerValues", RpcTarget.All);

        [PunRPC]
        private void RPCRefreshPlayerValues()
        {
            if (NameText != null)
                NameText.text = photonView.Owner.NickName;
            foreach (MeshRenderer renderer in ColourObjects)
            {
                renderer.material.color = JsonUtility.FromJson<Color>((string)photonView.Owner.CustomProperties["Colour"]);
            }
        }
    }

}