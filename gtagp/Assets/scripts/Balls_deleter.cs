using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balls
{
    public class Balls_deleter : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Object.Destroy(this.gameObject);
        }
    }
}