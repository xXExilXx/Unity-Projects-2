using UnityEngine;

internal class ColorStretcher : MonoBehaviour
{
    internal float hueSpeed = 1.0f;
    internal float minScale = 0.1f;
    internal float maxScale = 3f;
    internal float minSaturation = 2f;
    internal float maxSaturation = 3f;
    internal float minValue = 0.8f;
    internal float maxValue = 1.0f;

    internal Material material;
    internal float currentHue;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            currentHue = GetHueFromMaterialColor(material.color);
        }
        else
        {
            Debug.LogError("No Renderer component found. Script requires a Renderer component on the same GameObject.");
        }
    }

    void Update()
    {
        float yScale = Mathf.Clamp(transform.localScale.y, minScale, maxScale);
        currentHue += hueSpeed * yScale * Time.deltaTime;
        currentHue %= 1.0f;

        float saturation = Mathf.Lerp(minSaturation, maxSaturation, yScale / maxScale);
        float value = Mathf.Lerp(minValue, maxValue, yScale / maxScale);

        Color newColor = Color.HSVToRGB(currentHue, saturation, value);
        material.color = newColor;
    }

    internal float GetHueFromMaterialColor(Color color)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        return h;
    }
}
