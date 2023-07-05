using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravity : MonoBehaviour
{
    public new Rigidbody rigidbody;
    void OnTriggerStay(Collider other)
    {
        rigidbody.useGravity = false;
    }

    void OnTriggerExit(Collider other)
    {
        rigidbody.useGravity = true;
    }
}