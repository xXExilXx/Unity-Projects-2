using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class MediaController : MonoBehaviour
{
    private const int KEYEVENTF_EXTENDEDKEY = 1;
    private const int KEYEVENTF_KEYUP = 0;
    private const int VK_MEDIA_NEXT_TRACK = 0xB0;
    private const int VK_MEDIA_PLAY_PAUSE = 0xB3;
    private const int VK_MEDIA_PREV_TRACK = 0xB1;

    // Volume control keys
    private const int VK_VOLUME_UP = 0xAF;
    private const int VK_VOLUME_DOWN = 0xAE;

    [DllImport("user32.dll")]
    public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

    public Button playPauseButton;
    public Button skipForwardButton;
    public Button skipBackButton;
    public Slider volumeSlider;

    private float savedVolume;
    private bool isChangingVolume;

    void Start()
    {
        playPauseButton.onClick.AddListener(TogglePlayback);
        skipForwardButton.onClick.AddListener(SkipForward);
        skipBackButton.onClick.AddListener(SkipBack);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        volumeSlider.onValueChanged.AddListener(OnVolumeValueChanged);

        // Initialize the slider value with the current system volume
        savedVolume = volumeSlider.value; ;

        // Set the slider's min and max values
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
    }

    public void TogglePlayback()
    {
        SimulateMediaKey(VK_MEDIA_PLAY_PAUSE);
    }

    public void SkipForward()
    {
        SimulateMediaKey(VK_MEDIA_NEXT_TRACK);
    }

    public void SkipBack()
    {
        SimulateMediaKey(VK_MEDIA_PREV_TRACK);
    }

    private void OnVolumeValueChanged(float volume)
    {
        // Set a flag to indicate that volume is currently being changed
        isChangingVolume = true;
    }

    public void ChangeVolume(float volume)
    {
        // If volume is currently being changed, do nothing
        if (isChangingVolume)
        {
            return;
        }

        // Calculate the volume adjustment offset
        float volumeOffset = volume - savedVolume;

        // Adjust the system volume using the offset
        if (volumeOffset > 0)
        {
            for (int i = 0; i < Mathf.RoundToInt(volumeOffset * 100); i++)
            {
                SimulateMediaKey(VK_VOLUME_UP);
            }
        }
        else if (volumeOffset < 0)
        {
            for (int i = 0; i < Mathf.RoundToInt(Mathf.Abs(volumeOffset) * 100); i++)
            {
                SimulateMediaKey(VK_VOLUME_DOWN);
            }
        }

        // Save the new volume as the last saved volume
        savedVolume = volume;

        // Reset the flag as volume adjustment is complete
        isChangingVolume = false;
    }


    private void SimulateMediaKey(byte virtualKey)
    {
        keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, IntPtr.Zero);
        keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, IntPtr.Zero);
    }
}
