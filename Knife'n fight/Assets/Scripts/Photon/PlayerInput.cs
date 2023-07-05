using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerInput : MonoBehaviour
{
    public TMP_InputField input;
    public Button enter;
    public TextMeshProUGUI CurrentName;
    void Start()
    {
        enter.onClick.AddListener(Apply);
        if (PlayerPrefs.GetInt("WasEnterd") == 1)
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
            enter.gameObject.SetActive(false);
            input.enabled = false;
            CurrentName.text = PlayerPrefs.GetString("PlayerName");
        }
    }
        
    void Apply()
    {
        PhotonNetwork.NickName = input.text;
        CurrentName.text = input.text;
        PlayerPrefs.SetString("PlayerName", input.text);
        PlayerPrefs.SetInt("WasEnterd", 1);
        enter.gameObject.SetActive(false);
        input.enabled = false;
    }
}
