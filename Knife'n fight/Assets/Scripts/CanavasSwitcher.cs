using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanavasSwitcher : MonoBehaviour
{
    public GameObject OldCanavas;
    public GameObject NewCanavas;
    private bool isactive;

    public void Switch()
    {
        OldCanavas.SetActive(false);
        NewCanavas.SetActive(true);
        isactive = true;
        if (isactive)
        {
            OldCanavas.SetActive(true);
            NewCanavas.SetActive(false);
            isactive = false;
        }
    }
}
