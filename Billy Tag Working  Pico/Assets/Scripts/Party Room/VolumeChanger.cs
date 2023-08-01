using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public AudioSource Source;
    public Slider slider;
    void Start()
    {
        Source.volume = 0.3f;
        slider.maxValue = 0.55f;
        slider.value = 0.3f;
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    // Update is called once per frame
    void ChangeVolume(float value){
        Source.volume = value;
    }
}
