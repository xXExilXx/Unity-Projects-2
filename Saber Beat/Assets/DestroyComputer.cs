using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComputer : MonoBehaviour
{
    public bool DestroyComputerNow;
    void Update()
    {
        if(DestroyComputerNow){
            float bruh = Mathf.Infinity / 2;
            Debug.Log(bruh.ToString());
            DestroyComputerNow = false;
        }
    }
}
