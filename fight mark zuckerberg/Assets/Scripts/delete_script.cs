using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_script : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        Object.Destroy(this.gameObject);
    }
}