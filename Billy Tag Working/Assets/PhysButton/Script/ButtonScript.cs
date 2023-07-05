using Photon.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public AudioSource startSound;
    public AudioSource endSound;
    [Space]
    [Tooltip("Example what the button could be used for")]
    public GameObject ToggleObjectOnOrOff;
    [Space]
    public bool CustomCodeEnabeled;
    [Header("Custom Properties")]
    public string placeholder = "placeholder";

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(meshRenderer.material);
        material.color = Color.red;
        meshRenderer.material = material;
        startSound.Play();
        if (!CustomCodeEnabeled)
        {
            if (ToggleObjectOnOrOff != null)
            {
                if (ToggleObjectOnOrOff.gameObject.activeInHierarchy)
                {
                    ToggleObjectOnOrOff.SetActive(false);
                }
                else
                {
                    ToggleObjectOnOrOff.SetActive(true);
                }
            }
        }
        else
        {
            CustomCode();
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

    void CustomCode()
    {
        
    }
}
