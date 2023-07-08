using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public GameObject Player;
    public GameObject Score;
    public GameObject Button;
    public GameObject DeathScreen;
    public AudioSource DeathSound;

    // Update is called once per frame
    public void OnClick()
    {
        Destroy(Player);
        Destroy(Score);
        Destroy(Button);
        DeathScreen.SetActive(true);
        DeathSound.Play();
    }
}
