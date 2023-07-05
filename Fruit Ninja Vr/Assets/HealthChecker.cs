using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChecker : MonoBehaviour
{
    public Health health;
    public bool isdamage;
    
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Fruit")){
            if(isdamage){
                health.RemoveHealth();
            }
            else{
                health.GetHealth();
            }
        }
    }
}
