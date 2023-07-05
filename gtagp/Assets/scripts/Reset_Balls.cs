using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Balls
{
    public class Reset_Balls : MonoBehaviour
    {
        public GameObject spawnPoint;

        void OnTriggerEnter(Collider col)
        {
            if (col.transform.tag == "Ball")
            {
                this.transform.position = spawnPoint.transform.position;
            }
        }
    }
}