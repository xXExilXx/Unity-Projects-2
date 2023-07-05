using UnityEngine;
using System.IO;
using System;

public class CustomSounds : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        // Create a "Sounds" folder in the game's persistent data path if it doesn't already exist
        string soundFolderPath = Path.Combine(Application.persistentDataPath, "CustomShopMusic");
        if (!Directory.Exists(soundFolderPath))
        {
            Directory.CreateDirectory(soundFolderPath);
        }

        // Load the "custom_sound.wav" file from the "Sounds" folder if it exists
        string soundFilePath = Path.Combine(soundFolderPath, "custom_sound.wav");
        if (File.Exists(soundFilePath))
        {
            byte[] soundBytes = File.ReadAllBytes(soundFilePath);
            float[] soundSamples = new float[soundBytes.Length / 2];

            // Convert the byte array to a float array
            for (int i = 0; i < soundSamples.Length; i++)
            {
                short soundValue = BitConverter.ToInt16(soundBytes, i * 2);
                soundSamples[i] = soundValue / 32768f;
            }

            // Create an AudioClip and set it to the AudioSource
            AudioClip newClip = AudioClip.Create("Custom Sound", soundSamples.Length, 1, 44100, false);
            newClip.SetData(soundSamples, 0);
            audioSource.clip = newClip;
        }
    }
}
