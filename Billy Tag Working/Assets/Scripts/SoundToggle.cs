using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggle : MonoBehaviour
{
    public AudioSource Left;
    public AudioSource Right;

    private bool muted;
    private void OnTriggerEnter(Collider other)
    {
        if (muted)
        {
            Left.mute = false; Right.mute = false;
            muted = false;
        }
        else
        {
            Left.mute = true; Right.mute = true;
            muted = true;
        }
    }
}
    