using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource Music;
    public bool secondSpeake;
    public AudioSource Music2;

    private void Start()
    {
        Music.mute = true;
        if (secondSpeake)
        {
            Music2.mute = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Music.mute = false;
            if(secondSpeake)
            {
                Music2.mute = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Music.mute = true;
            if (secondSpeake)
            {
                Music2.mute = true;
            }
        }
    }
}
