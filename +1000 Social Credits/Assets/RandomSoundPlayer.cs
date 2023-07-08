using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip sound; // drag your audio clip here
    public float minDelay = 1f; // minimum delay between sounds
    public float maxDelay = 5f; // maximum delay between sounds

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }
}
