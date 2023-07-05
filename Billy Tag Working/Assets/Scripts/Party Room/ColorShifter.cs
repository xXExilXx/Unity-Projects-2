using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShifter : MonoBehaviour
{
    public Light partyLight;
    public float interval;
    private Color[] lightColors;
    private int currentColorIndex;
    private bool isBoolOn;

    private void Start()
    {
        lightColors = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow
        };
        currentColorIndex = 0;
        ChangeLightColor();
        StartCoroutine(ToggleBoolWithInterval());

    }

    private void Update()
    {
        if (isBoolOn)
        {
            currentColorIndex = (currentColorIndex + 1) % lightColors.Length;
            ChangeLightColor();
        }
    }
    private IEnumerator ToggleBoolWithInterval()
    {
        isBoolOn = true;
        isBoolOn = false;

        yield return new WaitForSeconds(interval);

        StartCoroutine(ToggleBoolWithInterval());
    }
    private void ChangeLightColor()
    {
        partyLight.color = lightColors[currentColorIndex];
    }
}
