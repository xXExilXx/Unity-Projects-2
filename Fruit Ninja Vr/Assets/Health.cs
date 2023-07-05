using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider.maxValue = 100f;
        slider.value = 100f;
    }

    public void RemoveHealth(){
        slider.value -= 10f;
    }
    
    public void GetHealth(){
        slider.value += 5f;
    }
}
