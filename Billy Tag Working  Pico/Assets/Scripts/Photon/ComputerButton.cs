using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerButton : MonoBehaviour
{
    public Computer computer;
    public AudioSource startSound;
    public AudioSource endSound;
    public bool LeftArrow;
    public bool RightArrow;
    public bool Enter;
    private bool isTriggered = false;

    private IEnumerator TriggerButton()
    {
        isTriggered = true;
        yield return new WaitForSeconds(0.5f);
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);
            material.color = Color.red;
            meshRenderer.material = material;
            startSound.Play();

            StartCoroutine(TriggerButton());

            if (LeftArrow)
            {
                computer.wasLeftArrowPressed = true;
            }
            if (RightArrow)
            {
                computer.wasRightArrowPressed = true;
            }
            if (Enter)
            {
                computer.wasEnterPressed = true;
            }
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