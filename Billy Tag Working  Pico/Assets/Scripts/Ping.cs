using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ping : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    private string FPS;
    private float deltaTime;
    void Start()
    {   
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        FPS = Mathf.Ceil(fps).ToString();
        m_TextMeshProUGUI.text = "Ping: " + PhotonNetwork.GetPing() + "\nFPS: " + FPS;
    }
}
