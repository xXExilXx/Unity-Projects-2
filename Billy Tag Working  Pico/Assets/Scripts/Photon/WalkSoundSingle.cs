using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundSingle : MonoBehaviour
{
    public AudioSource source;
    private void OnTriggerEnter(Collider other)
    {
        source.Play();
    }
}
