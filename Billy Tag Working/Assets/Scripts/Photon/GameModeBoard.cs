using UnityEngine;
using CustomButton;
using TMPro; 

public class GameModeBoard : MonoBehaviour
{
    public string CurrentGamemode;
    public TextMeshProUGUI CurrentGamemodeText;
    public Button DeathmatchButton;
    public Button DefaultButton;
    void Start()
    {
        if(PlayerPrefs.GetString("Gamemode") != null)
        {
            CurrentGamemode = PlayerPrefs.GetString("Gamemode");
            CurrentGamemodeText.text = CurrentGamemode;
        }
        else
        {
            PlayerPrefs.SetString("Gamemode", "Deathmatch");
            CurrentGamemode = PlayerPrefs.GetString("Gamemode");
            CurrentGamemodeText.text = CurrentGamemode;
            PlayerPrefs.Save();
        }
    }
    void Update()
    {
        if(DeathmatchButton.ispressed)
        {
            CurrentGamemode = "Deathmatch";
            CurrentGamemodeText.text = CurrentGamemode;
            PlayerPrefs.SetString("Gamemode", CurrentGamemode);
            PlayerPrefs.Save();
            DeathmatchButton.reset = true;
        }
        else if(DefaultButton.ispressed)
        {
            CurrentGamemode = "Default";
            CurrentGamemodeText.text = CurrentGamemode;
            PlayerPrefs.SetString("Gamemode", CurrentGamemode);
            PlayerPrefs.Save();
            DefaultButton.reset = true;
        }
    }
}