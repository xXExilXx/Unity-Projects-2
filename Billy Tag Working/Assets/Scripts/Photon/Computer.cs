using Photon.VR;
using TMPro;
using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public TMP_Text text;
    // Keyboard input
    public bool wasLeftArrowPressed;
    public bool wasRightArrowPressed;
    public bool wasEnterPressed;

    public TagManager manager;
    // Selection options
    private string[] _options = { "Join", "GameMode", "Leave" };
    private int _selectedOptionIndex = 0;

    // Queue options
    private string[] _queueOptions = { "Default", "Deathmatch" };
    private int _selectedQueueOptionIndex = 0;
    private string _selectedQueueOption = "Default";
    private const string SelectedQueueOptionKey = "SelectedQueueOption";

    private enum MenuState { Main, Queue };
    private MenuState _menuState = MenuState.Main;

    private void Start()
    {
        PhotonVRManager.SetUsername(System.Environment.MachineName);
        _selectedQueueOption = PlayerPrefs.GetString(SelectedQueueOptionKey);
        UpdateText();
    }
    private void Update()
    {
        if (_menuState == MenuState.Main)
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
                if (_selectedOptionIndex == 1) // Queue option is selected
                {
                    _menuState = MenuState.Queue;
                    _selectedQueueOptionIndex = 0;
                    _selectedQueueOption = _queueOptions[_selectedQueueOptionIndex];

                    // Save the selected queue option to PlayerPrefs
                    PlayerPrefs.SetString(SelectedQueueOptionKey, _selectedQueueOption);

                    UpdateText();
                }
                else if (_selectedOptionIndex == 2) // Leave option is selected
                {
                    PhotonNetwork.LeaveRoom();
                    manager.isReady = false;
                }
                else // Join option is selected
                {
                    PhotonVRManager.JoinRandomRoom(_selectedQueueOption, 10);
                    if(_selectedQueueOption == "Default")
                    {
                        manager.isReady = false;
                    }
                    if(_selectedQueueOption == "Deathmatch")
                    {
                        manager.isReady = true;
                    }
                }
                wasEnterPressed = false;
            }

        }
        else if (_menuState == MenuState.Queue)
        {
            // Handle queue options
            if (wasLeftArrowPressed)
            {
                _selectedQueueOptionIndex--;
                if (_selectedQueueOptionIndex < 0)
                {
                    _selectedQueueOptionIndex = _queueOptions.Length - 1;
                }
                _selectedQueueOption = _queueOptions[_selectedQueueOptionIndex];
                UpdateText();
                wasLeftArrowPressed = false;
            }
            else if (wasRightArrowPressed)
            {
                _selectedQueueOptionIndex++;
                if (_selectedQueueOptionIndex >= _queueOptions.Length)
                {
                    _selectedQueueOptionIndex = 0;
                }
                _selectedQueueOption = _queueOptions[_selectedQueueOptionIndex];
                UpdateText();
                wasRightArrowPressed = false;
            }
            else if (wasEnterPressed)
            {
                _menuState = MenuState.Main;
                UpdateText();
                wasEnterPressed = false;
            }
        }
    }
    private void UpdateText()
    {
        if (_menuState == MenuState.Main)
        {
            if (_selectedOptionIndex == 0) // Join option is selected
            {
                text.text = _options[_selectedOptionIndex];
            }
            else if (_selectedOptionIndex == 1) // Queue option is selected
            {
                text.text = $"{_options[_selectedOptionIndex]} - {_selectedQueueOption}";
            }
            else // Leave option is selected
            {
                text.text = _options[_selectedOptionIndex];
            }
        }
        else if (_menuState == MenuState.Queue)
        {
            text.text = $"{_queueOptions[_selectedQueueOptionIndex]}";
        }
    }
}