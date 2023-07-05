using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public int pointsToAdd = 10;

    private Score scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<Score>();
    }

    void OnTriggerEter(Collider other){
        scoreManager.AddScore(pointsToAdd);
    }
}
