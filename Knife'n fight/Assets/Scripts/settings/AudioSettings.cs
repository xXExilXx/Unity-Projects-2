using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioSource fxSource;
    public AudioSource bgmSource;
    public Slider fxVolumeSlider;
    public Slider bgmVolumeSlider;

    private const string FX_VOLUME_KEY = "fxVolume";
    private const string BGM_VOLUME_KEY = "bgmVolume";

    private void Start()
    {
        // Load saved settings or set default values
        float fxVolume = PlayerPrefs.GetFloat(FX_VOLUME_KEY, 0.75f);
        float bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.75f);

        // Set slider values based on saved settings
        fxVolumeSlider.value = fxVolume;
        bgmVolumeSlider.value = bgmVolume;

        // Set initial volumes
        SetFXVolume(fxVolume);
        SetBGMVolume(bgmVolume);

        // Add listener for FX volume slider changes
        fxVolumeSlider.onValueChanged.AddListener(SetFXVolume);

        // Add listener for BGM volume slider changes
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    private void OnDestroy()
    {
        // Save current settings on exit
        float fxVolume = GetFXVolume();
        float bgmVolume = GetBGMVolume();

        PlayerPrefs.SetFloat(FX_VOLUME_KEY, fxVolume);
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, bgmVolume);
        PlayerPrefs.Save();
    }

    private void SetFXVolume(float volume)
    {
        fxSource.volume = volume;
    }

    private float GetFXVolume()
    {
        return fxSource.volume;
    }

    private void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    private float GetBGMVolume()
    {
        return bgmSource.volume;
    }
}
