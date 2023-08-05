using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using GorillaLocomotion;

public class WalkSound : MonoBehaviour
{
    [Space]
    public AudioSource Left;
    public AudioSource Right;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.wasRightHandTouching)
        {
            Left.Play();
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
                if (capabilities.supportsImpulse)
                    device.SendHapticImpulse(0, 0.5f, 1.0f);
        }
        if (Player.Instance.wasLeftHandTouching)
        {
            Right.Play();
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
                if (capabilities.supportsImpulse)
                    device.SendHapticImpulse(0, 0.5f, 1.0f);
        }
    }

}
