using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SettingsApply : MonoBehaviour
{
    public Terrain terrain;
    public AudioSource fxSource;
    public AudioSource bgmSource;

    void Start()
    {
        float detailDistance = PlayerPrefs.GetFloat("DetailDistance");
        terrain.detailObjectDistance = detailDistance;
        float fxVolume = PlayerPrefs.GetFloat("fxVolume", 0.75f);
        float bgmVolume = PlayerPrefs.GetFloat("bgmVolume", 0.5f);

        SetFXVolume(fxVolume);
        SetBGMVolume(bgmVolume);

        int antiAliasing = PlayerPrefs.GetInt("AntiAliasing", 1);
        QualitySettings.antiAliasing = antiAliasing == 1 ? 2 : 0;

        int postProcessing = PlayerPrefs.GetInt("PostProcessing", 1);
        if (postProcessing == 0)
        {
            // Disable post-processing effects
            foreach (var v in FindObjectsOfType<PostProcessLayer>())
                v.enabled = false;
        }

        int textureQuality = PlayerPrefs.GetInt("TextureQuality", 2);
        QualitySettings.masterTextureLimit = textureQuality;

        int shadowQuality = PlayerPrefs.GetInt("ShadowQuality", 2);
        QualitySettings.shadowResolution = (ShadowResolution)shadowQuality;

        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);
        if (resolutionIndex != -1)
        {
            Resolution[] resolutions = Screen.resolutions;
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }

    private void SetFXVolume(float volume)
    {
        fxSource.volume = volume;
    }

    private void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
