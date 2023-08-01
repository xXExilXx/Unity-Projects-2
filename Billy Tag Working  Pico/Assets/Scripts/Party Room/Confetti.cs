using UnityEngine;

public class Confetti : MonoBehaviour
{
    public ParticleSystem confettiParticleSystem;
    public Color[] colorPalette;

    private void Start()
    {
        // Check if the required references are set
        if (confettiParticleSystem == null || colorPalette == null || colorPalette.Length == 0)
        {
            Debug.LogError("ConfettiParticleSetup: Missing references or color palette!");
            return;
        }

        // Set up the confetti particle system
        SetupConfettiParticleSystem();
    }

    private void SetupConfettiParticleSystem()
    {
        // Get the main module of the particle system
        ParticleSystem.MainModule mainModule = confettiParticleSystem.main;

        // Set the particle system properties
        mainModule.startSpeed = 5f;  // Adjust as desired
        mainModule.gravityModifier = 1f;  // Adjust as desired
        mainModule.startLifetime = new ParticleSystem.MinMaxCurve(3f, 5f);  // Adjust as desired

        // Create a color over lifetime module to randomize the confetti colors
        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = confettiParticleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;

        // Create a gradient color for the color over lifetime
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[colorPalette.Length];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[colorPalette.Length];

        // Set the color keys and alpha keys based on the color palette
        for (int i = 0; i < colorPalette.Length; i++)
        {
            float normalizedTime = (float)i / (colorPalette.Length - 1);
            colorKeys[i] = new GradientColorKey(colorPalette[i], normalizedTime);
            alphaKeys[i] = new GradientAlphaKey(colorPalette[i].a, normalizedTime);
        }

        gradient.SetKeys(colorKeys, alphaKeys);
        colorOverLifetime.color = gradient;
    }
}
