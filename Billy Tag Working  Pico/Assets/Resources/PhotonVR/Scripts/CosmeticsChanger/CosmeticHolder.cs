using Photon.VR;
using Photon.VR.Cosmetics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticHolder : MonoBehaviour
{
    public string Item;
    public CosmeticType Type;
    public AudioSource startSound;
    public AudioSource endSound;

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(meshRenderer.material);
        material.color = Color.red;
        meshRenderer.material = material;
        startSound.Play();
        PhotonVRManager.SetCosmetic(Type, "");
    }
    private void OnTriggerExit(Collider other)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(meshRenderer.material);
        material.color = Color.white;
        meshRenderer.material = material;
        endSound.Play();
        PhotonVRManager.SetCosmetic(Type, Item);
    }
}
