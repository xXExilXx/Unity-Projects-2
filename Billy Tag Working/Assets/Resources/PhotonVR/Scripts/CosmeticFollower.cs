using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticFollower : MonoBehaviour
{
    public Transform FollowerTransfrom;

    // Update is called once per frame
    void Update()
    {
        transform.position = FollowerTransfrom.position;
        transform.rotation = FollowerTransfrom.rotation;
    }
}