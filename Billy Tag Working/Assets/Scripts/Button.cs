using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomButton
{
    public class Button : MonoBehaviour
    {
        public bool ispressed { private set;  get; }
        public bool reset { set; private get; }
        public bool isDisabled { set ;get; }
        public AudioSource startSound;
        public AudioSource endSound;

        void OnTriggerEnter(Collider other)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);
            material.color = Color.red;
            meshRenderer.material = material;
            startSound.Play();
            if(!isDisabled )
            {
                ispressed = true;
            }
        }

        public void SetColor(Color color)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);
            material.color = color;
            meshRenderer.material = material;
        }
        private void Update()
        {
            if (reset)
            {
                ispressed = false;
                reset = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);
            material.color = Color.white;
            meshRenderer.material = material;
            endSound.Play();
        }
    }
}