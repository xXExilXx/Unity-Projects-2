using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TerrainSettings : MonoBehaviour
{
    public Terrain terrain;
    public TextMeshProUGUI detailDistanceText;
    public Slider detailDistanceSlider;
    public Toggle windToggle;

    private const string detailDistanceKey = "DetailDistance";
    private const string windToggleKey = "WindToggle";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(detailDistanceKey) && !PlayerPrefs.HasKey(windToggleKey))
        {
            detailDistanceSlider.value = detailDistanceSlider.maxValue / 2f;
            detailDistanceText.text = $"Detail Distance: {detailDistanceSlider.value}";
            windToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey(detailDistanceKey))
        {
            float detailDistance = PlayerPrefs.GetFloat(detailDistanceKey);
            terrain.detailObjectDistance = detailDistance;
            detailDistanceSlider.value = detailDistance;
            detailDistanceText.text = $"Detail Distance: {detailDistance}";
        }

        if (PlayerPrefs.HasKey(windToggleKey))
        {
            bool windEnabled = PlayerPrefs.GetInt(windToggleKey) == 1;
            terrain.drawTreesAndFoliage = windEnabled;
            windToggle.isOn = windEnabled;
        }

        // Subscribe to the OnValueChanged event of the detail distance slider
        detailDistanceSlider.onValueChanged.AddListener(OnDetailDistanceValueChanged);

        // Subscribe to the OnValueChanged event of the wind toggle
        windToggle.onValueChanged.AddListener(OnWindToggleValueChanged);
    }

    private void OnDetailDistanceValueChanged(float value)
    {
        // Update the detail object distance of the terrain component
        terrain.detailObjectDistance = value;

        // Update the text on the detail distance label to reflect the new value
        detailDistanceText.text = $"Detail Distance: {terrain.detailObjectDistance}";

        // Save the new detail distance setting to PlayerPrefs
        PlayerPrefs.SetFloat(detailDistanceKey, terrain.detailObjectDistance);
    }

    private void OnWindToggleValueChanged(bool value)
    {
        // Enable/disable the wind effect on the terrain component
        terrain.drawTreesAndFoliage = value;

        // Save the new wind setting to PlayerPrefs
        PlayerPrefs.SetInt(windToggleKey, value ? 1 : 0);
    }
}
