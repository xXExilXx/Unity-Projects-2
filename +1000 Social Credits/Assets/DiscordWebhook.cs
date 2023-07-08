using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;

public class DiscordWebhook : MonoBehaviour
{
    public string webhookURL;
    public string playerName = "New User";
    private PlayerNameInput inputfield;
    public bool Reset;

    private void Start()
    {
        inputfield = GetComponent<PlayerNameInput>();
    }

    private void Update()
    {
        if (Reset)
        {
            PlayerPrefs.DeleteKey("HasStartedGame");
            Reset = false;
        }
        if (inputfield.isEntered)
        {
            StartCoroutine(SendMessage());
        }
    }

    private IEnumerator SendMessage()
    {
        // Check if the player has started the game before
        if (PlayerPrefs.HasKey("HasStartedGame"))
        {
            yield break;
        }

        // Set the "HasStartedGame" key to indicate that the player has started the game
        PlayerPrefs.SetInt("HasStartedGame", 1);

        // Format the message to be sent to the webhook
        string json = "{\"content\":\"" + playerName + " has started playing the game for the first time!\"}";

        // Create a web request to the Discord webhook URL
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webhookURL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.UserAgent = "Unity";

        // Write the JSON message to the request body
        using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        // Send the request and wait for the response
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
        {
            string responseText = streamReader.ReadToEnd();
            Debug.Log("Discord webhook response: " + responseText);
        }

        yield return null;
    }
}
