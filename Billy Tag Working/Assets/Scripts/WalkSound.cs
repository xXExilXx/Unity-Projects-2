using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public GorillaLocomotion.Player locomotion;
    [Space]
    public AudioSource Left;
    public AudioSource Right;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (locomotion.wasRightHandTouching)
        {
            Left.Play();
        }
        if (locomotion.wasLeftHandTouching)
        {
            Right.Play();
        }
    }
}
