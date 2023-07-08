using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public bool play;
    public NoteSpawner spawner;

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            spawner.Play();
            play = false;
        }
    }
}
