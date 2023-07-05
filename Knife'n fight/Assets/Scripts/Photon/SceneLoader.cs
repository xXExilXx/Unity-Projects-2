using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class SceneLoader : MonoBehaviour
{
    public Dropdown dropdown;
    public Button startButton;
    public TextMeshProUGUI statustext;
    public bool BigMap;

    private void Start()
    {
        // Add options to the dropdown menu
        dropdown.options.Add(new Dropdown.OptionData("Select Map"));
        dropdown.options.Add(new Dropdown.OptionData("Thicc boi"));
        dropdown.options.Add(new Dropdown.OptionData("Schmal boi"));

        // Set the initial selected option
        dropdown.value = 0;
        dropdown.RefreshShownValue();

        // Add a listener to the start button
        startButton.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {

        int selectedScene = dropdown.value;

        // Load the scene based on the selected option
        if (selectedScene == 1)
        {
            BigMap = true;
            PhotonNetwork.LoadLevel(1);
        }
        else if (selectedScene == 3)
        {
            BigMap = false;
            PhotonNetwork.LoadLevel(2);
        }
        else if (selectedScene == 0)
        {
            statustext.text = "Select a Map first!";
        }
    }
}
