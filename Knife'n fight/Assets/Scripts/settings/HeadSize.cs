using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadSize : MonoBehaviour
{
    public GameObject character; // reference to the character game object
    public Slider slider; // reference to the slider UI element

    private float minHeadSize = 1f; // minimum head size
    private float maxHeadSize = 2f; // maximum head size

    void Start()
    {
        // set the slider value to the current head size of the character
        slider.value = character.transform.localScale.y;
    }

    public void OnHeadSizeSliderChanged()
    {
        // get the slider value and map it to the head size range
        float headSize = Mathf.Lerp(minHeadSize, maxHeadSize, slider.value);

        // set the character's head size
        Vector3 scale = character.transform.localScale;
        scale.y = headSize;
        character.transform.localScale = scale;
    }
}
