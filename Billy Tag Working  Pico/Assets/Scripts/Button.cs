using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomButton
{
    public class Button : MonoBehaviour
    {
       public bool ispressed;
       public AudioSource startSound;
        public AudioSource endSound;

        void OnTriggerEnter(Collider other)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);
            material.color = Color.red;
            meshRenderer.material = material;
            startSound.Play();
            ispressed = true;
        }

        private void OnTriggerExit(Collider other)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);
            material.color = Color.white;
            meshRenderer.material = material;
            endSound.Play();
            ispressed = false;
        }
    }
}