using Photon.VR;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class Computer : MonoBehaviour
{
    public TMP_Text text;
    // Keyboard input
    public bool wasLeftArrowPressed;
    public bool wasRightArrowPressed;
    public bool wasEnterPressed;

    public TagManager manager;
    public GameModeBoard gmb;
    public motd modttext;
    // Selection options
    private string[] _options = { "Motd", "Join", "Leave", "UserData"};
    private int _selectedOptionIndex = 0;

    private void Start()
    {
        if(PlayerPrefs.GetString("Username") != System.Environment.UserName)
        {
             PhotonVRManager.SetUsername(System.Environment.UserName);
        }
        UpdateText();
    }

    private void Update()
    {
        // Handle selection options
        if (wasLeftArrowPressed)
        {
            _selectedOptionIndex--;
            if (_selectedOptionIndex < 0)
            {
                _selectedOptionIndex = _options.Length - 1;
            }
            UpdateText();
            wasLeftArrowPressed = false;
        }
        else if (wasRightArrowPressed)
        {
            _selectedOptionIndex++;
            if (_selectedOptionIndex >= _options.Length)
            {
                _selectedOptionIndex = 0;
            }
            UpdateText();
            wasRightArrowPressed = false;
        }
        else if (wasEnterPressed)
        {
            if (_selectedOptionIndex == 2) // Leave option is selected
            {
                PhotonNetwork.LeaveRoom();
            }
            else if(_selectedOptionIndex == 1)// Join option is selected
            {
                PhotonVRManager.JoinRandomRoom(gmb.CurrentGamemode, 10);
            }
            wasEnterPressed = false;
        }
    }

    private void UpdateText()
    {
        if(_selectedOptionIndex == 0)
        {
            text.text = $"Today Message of the day: \n" + modttext.ModtString;
        }
        else if (_selectedOptionIndex == 1) // Join option is selected
        {
            text.text = _options[_selectedOptionIndex];
        }
        else if(_selectedOptionIndex == 2) // Leave option is selected
        {
            text.text = _options[_selectedOptionIndex];
        }
        else if(_selectedOptionIndex == 3)
        {
            text.text = $"User Data:\n" + PhotonNetwork.LocalPlayer.CustomProperties["PlayFabPlayerID"];
        }
    }
}