using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class GraphicsSettings : MonoBehaviour
{
    public Toggle antiAliasingToggle;
    public Toggle postProcessingToggle;
    public Slider textureQualitySlider;
    public Slider shadowQualitySlider;
    public Dropdown resolutionDropdown;

    private const string AntiAliasingPrefKey = "AntiAliasing";
    private const string PostProcessingPrefKey = "PostProcessing";
    private const string TextureQualityPrefKey = "TextureQuality";
    private const string ShadowQualityPrefKey = "ShadowQuality";
    private const string ResolutionPrefKey = "ResolutionIndex";

    private Resolution[] resolutions;

    private void Start()
    {

        if (!PlayerPrefs.HasKey(AntiAliasingPrefKey) && !PlayerPrefs.HasKey(PostProcessingPrefKey) && !PlayerPrefs.HasKey(TextureQualityPrefKey) && !PlayerPrefs.HasKey(ShadowQualityPrefKey) && !PlayerPrefs.HasKey(ResolutionPrefKey))
        {
            antiAliasingToggle.isOn = true;
            postProcessingToggle.isOn = true;
            textureQualitySlider.value = textureQualitySlider.maxValue / 2f;
            shadowQualitySlider.value = shadowQualitySlider.maxValue / 2f;
        }

        antiAliasingToggle.isOn = PlayerPrefs.GetInt(AntiAliasingPrefKey, 1) == 1;
        postProcessingToggle.isOn = PlayerPrefs.GetInt(PostProcessingPrefKey, 1) == 1;
        textureQualitySlider.value = PlayerPrefs.GetInt(TextureQualityPrefKey, 2);
        shadowQualitySlider.value = PlayerPrefs.GetInt(ShadowQualityPrefKey, 2);

        // Load the available screen resolutions and populate the resolution dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = PlayerPrefs.GetInt(ResolutionPrefKey, -1);
        for (int i = 0; i < resolutions.Length; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData($"{resolutions[i].width}x{resolutions[i].height}");
            resolutionDropdown.options.Add(option);
            if (i == currentResolutionIndex)
            {
                resolutionDropdown.value = i;
            }
        }

        ApplyGraphicsSettings();
    }

    public void OnAntiAliasingChanged(bool value)
    {
        // Update the anti-aliasing graphics setting based on the toggle value
        PlayerPrefs.SetInt(AntiAliasingPrefKey, value ? 1 : 0);
        PlayerPrefs.Save();
        ApplyGraphicsSettings();
    }

    public void OnPostProcessingChanged(bool value)
    {
        // Update the post-processing graphics setting based on the toggle value
        PlayerPrefs.SetInt(PostProcessingPrefKey, value ? 1 : 0);
        PlayerPrefs.Save();
        ApplyGraphicsSettings();
    }

    public void OnTextureQualityChanged(float value)
    {
        // Update the texture quality graphics setting based on the slider value
        PlayerPrefs.SetInt(TextureQualityPrefKey, (int)value);
        PlayerPrefs.Save();
        ApplyGraphicsSettings();
    }

    public void OnShadowQualityChanged(float value)
    {
        // Update the shadow quality graphics setting based on the slider value
        PlayerPrefs.SetInt(ShadowQualityPrefKey, (int)value);
        PlayerPrefs.Save();
        ApplyGraphicsSettings();
    }

    public void OnResolutionChanged(int index)
    {
        // Update the game's resolution based on the selected resolution index
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(ResolutionPrefKey, index);
        PlayerPrefs.Save();
    }

    private void ApplyGraphicsSettings()
    {
        // Apply the current graphics settings to the game
        QualitySettings.antiAliasing = antiAliasingToggle.isOn ? 2 : 0;
        QualitySettings.masterTextureLimit = (int)textureQualitySlider.value;
        QualitySettings.shadowResolution = (ShadowResolution)(int)shadowQualitySlider.value;
    }
}
