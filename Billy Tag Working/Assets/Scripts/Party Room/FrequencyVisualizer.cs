using UnityEngine;
using System.Collections;

public class FrequencyVisualizer : MonoBehaviour
{
    public GameObject barPrefab;
    public float barSpacing = 0.1f;
    public float maxHeight = 10f;
    public int barCount = 64;

    public AudioSource audioSource;
    public float startFrequency = 20f;
    public float endFrequency = 2000f;
    
    [Range(5f, 40f)]
    public float sensitivity = 1f; // Added sensitivity value with a range attribute

    private GameObject[] bars;
    private float[] spectrumData;
    private bool isReady;

    private void Start()
    {
        transform.Rotate(Vector3.forward, 180, Space.Self);
        StartCoroutine(CreateBars());
        spectrumData = new float[1024];
    }

    private void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        UpdateBars();
    }

    IEnumerator CreateBars()
    {
        bars = new GameObject[barCount];
        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < barCount; i++)
        {
            yield return new WaitForSeconds(0.005f);
            Vector3 position = spawnPosition + new Vector3(i * barSpacing, 0f, 0f);
            GameObject bar = Instantiate(barPrefab, position, Quaternion.identity, transform);
            bars[i] = bar;
        }
        isReady = true;
    }

    private void UpdateBars()
    {
        int spectrumDataLength = spectrumData.Length;
        if(isReady)
        {
            for (int i = 0; i < barCount; i++)
            {
                // Calculate the frequency range indices for the current bar
                float barStartFrequency = i * (endFrequency - startFrequency) / barCount + startFrequency;
                float barEndFrequency = (i + 1) * (endFrequency - startFrequency) / barCount + startFrequency;
                int barStartIndex = (int)(barStartFrequency * spectrumDataLength / AudioSettings.outputSampleRate);
                int barEndIndex = (int)(barEndFrequency * spectrumDataLength / AudioSettings.outputSampleRate);

                // Calculate the average intensity within the frequency range for the current bar
                float averageIntensity = 0f;
                int sampleCount = 0;

                for (int j = barStartIndex; j <= barEndIndex; j++)
                {
                    averageIntensity += spectrumData[j];
                    sampleCount++;
                }

                if (sampleCount > 0)
                {
                    averageIntensity /= sampleCount;
                }

                // Apply the intensity to the current bar with sensitivity
                GameObject bar = bars[i];
                float height = Mathf.Clamp(averageIntensity * maxHeight * sensitivity, 0f, maxHeight);
                bar.transform.localScale = new Vector3(0.13f, height, 0.00013f);
            }
        }
    }
}