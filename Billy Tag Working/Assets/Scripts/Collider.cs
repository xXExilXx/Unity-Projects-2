using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomCollider
{
    public class Collider : MonoBehaviour
    {
        public bool istouched;
        public string tag {set;get;}
        [HideInInspector] public static bool onetick = true;
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            if (other.CompareTag(tag))
            {
                if (onetick)
                {
                    istouched = true;
                    istouched = false;
                }
                else
                {
                    istouched = true;
                }
            }
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            if (other.CompareTag(tag))
            {
                if (!onetick)
                {
                    istouched = false;
                }
            }
        }
    }
}
