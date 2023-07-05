using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Follower{
    public class LocalPlayer : MonoBehaviour
    {
        public static LocalPlayer Manager { get; private set; }
        public Transform Body;
        public Transform Hip;
    }
} 