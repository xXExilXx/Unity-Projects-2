using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShifter : MonoBehaviour
{
    public Light LightSource;
    public List<Color> Colors = new List<Color>();
    public float interval;
    private float lastInterval;
    void Start()
    {
        lastInterval = Time.time;
    }
    void Update()
    {
        float TimeOver = Time.time - lastInterval;
        if(TimeOver >= interval)
        {
            if(Colors.Count > 0)
            {
                int ColorIndex = Random.Range(0, Colors.Count);
                Color randomColor = Colors[ColorIndex];
                LightSource.color = randomColor;
            }
            
            lastInterval = Time.time;
        }
    }
}
