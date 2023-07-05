using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Backroms : MonoBehaviour
{
    [SerializeField] string newGameScene;
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene(newGameScene);
        }
    }
}
