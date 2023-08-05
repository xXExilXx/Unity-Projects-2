using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveMusicMixer : MonoBehaviour
{
    public AudioClip[] musicClips;
    public float mainVolume = 1f;

    public FrequencyVisualizer frequencyVisualizer; // Reference to the FrequencyVisualizer script
    public MusicPlayer player;

    public float crossfadeDuration = 5f;

    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private int currentClipIndex = 0;
    private bool isPlaying = false;

    private void Start()
    {
        player.secondSpeake = true;
        
        audioSource1 = gameObject.AddComponent<AudioSource>();
        player.Music = audioSource1;
        audioSource1.loop = false;
        audioSource1.volume = mainVolume;

        audioSource2 = gameObject.AddComponent<AudioSource>();
        player.Music2 = audioSource2;
        audioSource2.loop = false;
        audioSource2.volume = 0f; // Start with volume set to 0

        StartCoroutine(StartMixing());
    }

    private IEnumerator StartMixing()
    {
        if(player.Music && player.Music2 == null)
        {
            player.Music = audioSource1;
            player.Music2 = audioSource2;
        }
        
        isPlaying = true;
        while (isPlaying)
        {
            AudioClip currentClip = musicClips[currentClipIndex];
            audioSource1.clip = currentClip;
            frequencyVisualizer.audioSource = audioSource1;
            audioSource1.Play();

            // Update the FrequencyVisualizer with the current audio source
            if (frequencyVisualizer != null)
            {
                frequencyVisualizer.audioSource = audioSource1;
            }

            float crossfadePoint = currentClip.length - crossfadeDuration;
            float pitchNormalizePoint = currentClip.length / 2f;

            // Wait until it's time to start the crossfade
            while (audioSource1.time < crossfadePoint)
            {
                yield return null;
            }

            // Start the crossfade and preload the next song in audioSource2
            audioSource2.clip = musicClips[(currentClipIndex + 1) % musicClips.Length];
            audioSource2.volume = mainVolume;
            frequencyVisualizer.audioSource = audioSource2;
            audioSource2.Play();
            frequencyVisualizer.audioSource = audioSource2;

            // Crossfade
            float crossfadeTimer = 0f;
            while (crossfadeTimer < crossfadeDuration)
            {
                crossfadeTimer += Time.deltaTime;
                float normalizedVolume = crossfadeTimer / crossfadeDuration;
                audioSource1.volume = mainVolume * (1f - normalizedVolume);
                audioSource2.volume = mainVolume * normalizedVolume;

                yield return null;
            }

            audioSource1.Stop();

            // Move to the next clip
            currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
        }
    }

    public void StopMixing()
    {
        isPlaying = false;
        StopAllCoroutines();
        audioSource1.Stop();
        audioSource2.Stop();
    }
}
