using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomButton;

public class GameModeBoard : MonoBehaviour
{
    public string CurrentGamemode;
    public Button DeathmatchButton;
    public Button DefaultButton;

    void Start()
    {
        CurrentGamemode = PlayerPrefs.GetString("Gamemode", "Deathmatch");
    }

    void Update()
    {
        if(DeathmatchButton.ispressed)
        {
            CurrentGamemode = "Deathmatch";
            PlayerPrefs.SetString("Gamemode", CurrentGamemode);
        }
        if(DefaultButton.ispressed)
        {
            CurrentGamemode = "Default";
            PlayerPrefs.SetString("Gamemode", CurrentGamemode);
        }
    }
}