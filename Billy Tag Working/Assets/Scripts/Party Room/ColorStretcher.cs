using UnityEngine;

internal class ColorStretcher : MonoBehaviour
{
    internal float hueSpeed = 1.0f;
    internal float minScale = 0.1f;
    internal float maxScale = 3f;
    internal float minSaturation = 0.5f; // Adjust this value to control the minimum saturation.
    internal float maxSaturation = 4f;
    internal float minValue = 0.2f; // Adjust this value to control the minimum value (brightness).
    internal float maxValue = 1.0f;

    internal Material material;
    internal float currentHue;
    internal float originalSaturation;
    internal float originalValue;
    internal float previousYScale;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            originalSaturation = GetSaturationFromMaterialColor(material.color);
            originalValue = GetValueFromMaterialColor(material.color);

            // Set the initial yScale and previousYScale to avoid unnecessary color updates.
            previousYScale = Mathf.Clamp(transform.localScale.y, minScale, maxScale);

            // Set a random initial hue value.
            currentHue = Random.Range(0f, 1f);
        }
        else
        {
            Debug.LogError("No Renderer component found. Script requires a Renderer component on the same GameObject.");
        }
    }

    void Update()
    {
        float yScale = Mathf.Clamp(transform.localScale.y, minScale, maxScale);

        // Check if yScale has changed from the previous frame.
        if (yScale != previousYScale)
        {
            currentHue += hueSpeed * Time.deltaTime;
            currentHue %= 1.0f;

            float saturation = Mathf.Lerp(minSaturation, maxSaturation, yScale / maxScale);
            float value = Mathf.Lerp(minValue, maxValue, yScale / maxScale);

            Color newColor = Color.HSVToRGB(currentHue, saturation, value);
            material.color = newColor;

            // Update the previousYScale to the current yScale.
            previousYScale = yScale;
        }
    }

    internal float GetSaturationFromMaterialColor(Color color)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        return s;
    }

    internal float GetValueFromMaterialColor(Color color)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        return v;
    }
}
