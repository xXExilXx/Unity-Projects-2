using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    void Update()
    {
        float currentHour = System.DateTime.Now.Hour;
        float currentMinute = System.DateTime.Now.Minute;
        float currentSecond = System.DateTime.Now.Second;

        float hourAngle = (currentHour % 12) * 30f + currentMinute * 0.5f;
        float minuteAngle = currentMinute * 6f;
        float secondAngle = currentSecond * 6f;

        hourHand.localRotation = Quaternion.Euler(0f, 0f, hourAngle);
        minuteHand.localRotation = Quaternion.Euler(0f, 0f, minuteAngle);
        secondHand.localRotation = Quaternion.Euler(0f, 0f, secondAngle);
    }
}
