using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocalPlayerFollower
{
    public class LocalPlayer : MonoBehaviour
    {
        public static LocalPlayer instance;
        public Transform Head;
        public Transform LeftHand;
        public Transform RightHand;
        void Start()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
    }
}