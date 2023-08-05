using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalPlayerFollower;

public class PlayerFollower1 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(LocalPlayer.instance.Head.position);
    }
}
