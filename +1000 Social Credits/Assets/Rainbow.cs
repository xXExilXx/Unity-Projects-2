using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rainbow : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        StartCoroutine(ShiftColors());
    }

    IEnumerator ShiftColors()
    {
        int colorIndex = 0;
        Color[] colors = { Color.red, Color.yellow, Color.green, Color.blue };

        while (true)
        {
            text.color = colors[colorIndex];
            colorIndex = (colorIndex + 1) % colors.Length;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
