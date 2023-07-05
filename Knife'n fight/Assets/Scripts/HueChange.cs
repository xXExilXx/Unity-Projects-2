using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HueChange : MonoBehaviour
{
    public PostProcessVolume volume;
    public float hueShiftSpeed = 10f;
    private ColorGrading colorGradingLayer;
    private float currenthue = 10f;
    private float hueShiftAmount;
    private float targetHueShiftAmount;
    public bool cycling = false;
    public bool L;
    public bool S;
    public bool D;

    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
        currenthue = colorGradingLayer.hueShift.value;
        if(PlayerPrefs.GetInt("HUE") == 1)
        {
            cycling = true;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            L = true;
            L = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            S = true;
            S = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            D = true;
            D = false;
        }
        
        if(L && S && D)
        {
            if (cycling)
            {
                cycling = false;
            }
            else if(!cycling)
            {
                cycling = true;
            }
        }

        if (cycling)
        {
            hueShiftAmount = Mathf.MoveTowards(hueShiftAmount, targetHueShiftAmount, hueShiftSpeed * Time.deltaTime);

            colorGradingLayer.hueShift.value = hueShiftAmount;

            // If the current hue shift reaches the target value, set the next target value
            if (hueShiftAmount == targetHueShiftAmount)
            {
                targetHueShiftAmount += 30f;

                // Wrap the target hue shift around from 180 to -180 degrees
                if (targetHueShiftAmount > 180f)
                {
                    targetHueShiftAmount -= 360f;
                }
            }
            PlayerPrefs.SetInt("HUE", 1);
        }
        else
        {
            colorGradingLayer.hueShift.value = currenthue;
            PlayerPrefs.SetInt("HUE", 0);
        }
    }
}
