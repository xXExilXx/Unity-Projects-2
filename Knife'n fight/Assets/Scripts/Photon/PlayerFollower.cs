using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Follower{
    public class PlayerFollower : MonoBehaviour
    {
        public Transform Body;
        public Transform Spine;
        public GameObject Player;
        private PhotonView view;
        void Start()
        {
            view = GetComponent<PhotonView>();
            if(view.IsMine){
                Player.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(view.IsMine){
                Body.transform.position = LocalPlayer.Manager.Body.transform.position;
                Spine.transform.position = LocalPlayer.Manager.Hip.transform.position;
                Body.transform.rotation = LocalPlayer.Manager.Body.transform.rotation;
                Spine.transform.rotation = LocalPlayer.Manager.Hip.transform.rotation;
            }
        }
    }

}