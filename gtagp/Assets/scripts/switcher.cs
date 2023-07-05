using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switcher : MonoBehaviour
{
    public string newGameScene;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene(newGameScene);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }
}