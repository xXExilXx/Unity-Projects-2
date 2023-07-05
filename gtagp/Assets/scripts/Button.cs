using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Mesh;
    public Material Triggerd;
    public Material Default;
    public AudioSource Enter;
    public AudioSource Exit;

    private void OnTriggerEnter(Collider other)
    {
        Enter.Play();
    }
    private void OnTriggerStay(Collider other)
    {
        GetComponent<MeshRenderer>().material = Triggerd;
        if (Mesh.activeInHierarchy == true)
            Mesh.SetActive(false);
        else
            Mesh.SetActive(true);

    }
    private void OnTriggerExit(Collider other)
    {
        Exit.Play();
        GetComponent<MeshRenderer>().material = Default;
    }
}