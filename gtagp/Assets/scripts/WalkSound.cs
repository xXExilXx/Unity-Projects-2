using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    [Tooltip("Put the root of your Vr Controller in here")]
    public Transform Controller;
    [Tooltip("Put a Audio Source in here that contains the audio u wanna Play")]
    public AudioSource audioSource;
    [Tooltip("The offset for your arm Length")]
    public Vector3 Offset;
    void Update()
    {
        transform.SetPositionAndRotation(Controller.position + Offset, Controller.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
    }
}
