using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Spawner;
    public GameObject Score;
    public GameObject Button;
    public GameObject DeathScreen;
    public AudioSource DeathSound;
    private GameObject[] gameObjects;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(Player);
            Destroy(Spawner);
            Destroy(Score);
            Destroy(Button);
            DestroyAllObjects();
            DeathScreen.SetActive(true);
            DeathSound.Play();
        }
    }

    void DestroyAllObjects()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
