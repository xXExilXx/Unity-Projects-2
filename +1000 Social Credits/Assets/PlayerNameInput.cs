using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject startbutton;
    private DiscordWebhook discordWebhook;
    public bool isEntered = false;
    private void Start()
    {
        discordWebhook = GetComponent<DiscordWebhook>();
        inputField.onEndEdit.AddListener(AcceptPlayerName);
        if (PlayerPrefs.GetInt("IsEnterd") == 1)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            startbutton.SetActive(false);
        }
    }

    private void AcceptPlayerName(string playerName)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            discordWebhook.playerName = playerName;
            Debug.Log(playerName);
            inputField.gameObject.SetActive(false);
            startbutton.SetActive(true);
            PlayerPrefs.SetInt("isEnterd", 0);
            isEntered = true;
        }
    }
}
