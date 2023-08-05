using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.VR;
public class ActionManager : MonoBehaviour
{
    public GameObject board;
    public XRNode hand;

    bool wasButtonPressed;
    bool isFollowing;
    
    void Update()
    {
        bool primaryButtonPressed;
        InputDevice device = InputDevices.GetDeviceAtXRNode(hand);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed);
        if (primaryButtonPressed && wasButtonPressed)
        {
            EnableMenu();
            wasButtonPressed = false;
        }
        else if (!wasButtonPressed && primaryButtonPressed)
        {
            DisableMenu();
            wasButtonPressed = true;
        }
        else if(isFollowing)
        {
            board.transform.LookAt(PhotonVRManager.Manager.Head.position);
        }
    }

    void EnableMenu()
    {
        isFollowing = true;
        board.SetActive(true);
    }

    void DisableMenu()
    {
        isFollowing = false;
        board.SetActive(false);
    }
}
