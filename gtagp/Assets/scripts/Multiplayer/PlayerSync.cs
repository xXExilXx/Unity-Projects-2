using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviour
{
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position;
        transform.rotation = Target.rotation;
    }
}