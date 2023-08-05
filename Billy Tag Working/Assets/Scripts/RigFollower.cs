using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigfollower : MonoBehaviour
{
    public Transform SteamVRRig;
    public Transform OpenXRRig;

    // Update is called once per frame
    void Update()
    {
        if (SteamVRRig.gameObject.active)
        {
            OpenXRRig.position = SteamVRRig.position;
            OpenXRRig.rotation = SteamVRRig.rotation;
        }
        else if(OpenXRRig.gameObject.active)
        {
            SteamVRRig.position = OpenXRRig.position;
            SteamVRRig.rotation = OpenXRRig.rotation;
        }
    }
}
