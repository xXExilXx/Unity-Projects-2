using UnityEngine;

public enum FrequencyPreset
{
    SubBass,
    Bass,
    Middle,
    High,
    HighEnd
}

public class AudioEmissionController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Renderer targetRenderer;
    [SerializeField]
    private Color emissiveColor = Color.white;
    [SerializeField]
    [Range(1, 500)]
    private float sensitivity = 100f;
    [SerializeField]
    [Range(0, 1)]
    private float smoothing = 0.5f;
    [SerializeField]
    private float threshold = 0.01f;
    [SerializeField]
    private string emissionPropertyName = "_EmissionColor";
    [SerializeField]
    private float defaultIntensity = 0f;
    [SerializeField]
    private FrequencyPreset frequencyPreset = FrequencyPreset.Middle;

    private Material material;
    private int emissionPropertyID;
    private float currentIntensity;
    private float[][] frequencyRanges = {
        new float[] { 20f, 60f },
        new float[] { 60f, 250f },
        new float[] { 250f, 2000f },
        new float[] { 2000f, 8000f },  // Added HighEnd frequency range
        new float[] { 8000f, 20000f }
    };

    private void Start()
    {
        targetRenderer = GetComponent<Renderer>();
    }

    private void Awake()
    {
        material = targetRenderer.material;
        emissionPropertyID = Shader.PropertyToID(emissionPropertyName);
    }

    private void Update()
    {
        float[] spectrumData = new float[1024];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        int presetIndex = (int)frequencyPreset;
        float[] frequencyRange = frequencyRanges[presetIndex];

        int startFrequencyIndex = (int)(frequencyRange[0] * spectrumData.Length / AudioSettings.outputSampleRate);
        int endFrequencyIndex = (int)(frequencyRange[1] * spectrumData.Length / AudioSettings.outputSampleRate);

        float maxAmplitude = 0f;
        for (int i = startFrequencyIndex; i < endFrequencyIndex; i++)
        {
            maxAmplitude = Mathf.Max(maxAmplitude, spectrumData[i]);
        }

        if (maxAmplitude > threshold)
        {
            float targetIntensity = maxAmplitude * sensitivity;
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, smoothing);
        }
        else
        {
            currentIntensity = Mathf.Lerp(currentIntensity, defaultIntensity, smoothing);
        }

        material.SetColor(emissionPropertyID, emissiveColor * currentIntensity);
    }

    private void OnDestroy()
    {
        if (material != null)
        {
            Destroy(material);
        }
    }
}