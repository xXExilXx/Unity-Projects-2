using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class motd : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string motdUrl = "http://example.com/motd.txt"; // the URL where the MOTD is located
    [SerializeField] private float refreshInterval = 60f; // how often to refresh the MOTD in seconds
    [SerializeField] private bool autoRefresh = true; // whether to automatically refresh the MOTD on an interval

    [Header("UI")]
    [SerializeField] private Text motdText; // a UI Text object to display the MOTD

    private string cachedMotd; // the cached MOTD
    private float lastRefreshTime; // the time the MOTD was last refreshed

    private void Start()
    {
        if (autoRefresh)
        {
            StartCoroutine(RefreshMotdRoutine());
        }
        else
        {
            StartCoroutine(LoadMotdRoutine());
        }
    }

    private IEnumerator LoadMotdRoutine()
    {
        UnityWebRequest www = UnityWebRequest.Get(motdUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning("Failed to load MOTD: " + www.error);
            LoadCachedMotd();
            yield break;
        }

        string motd = www.downloadHandler.text;
        Debug.Log("MOTD: " + motd);

        cachedMotd = motd;
        lastRefreshTime = Time.time;

        if (motdText != null)
        {
            motdText.text = motd;
        }
    }

    private IEnumerator RefreshMotdRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(refreshInterval);

            UnityWebRequest www = UnityWebRequest.Get(motdUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Failed to refresh MOTD: " + www.error);
                continue;
            }

            string motd = www.downloadHandler.text;
            Debug.Log("MOTD refreshed: " + motd);
            cachedMotd = motd;
            lastRefreshTime = Time.time;

            if (motdText != null)
            {
                motdText.text = motd;
            }
        }
    }

    private void LoadCachedMotd()
    {
        if (cachedMotd != null)
        {
            if (motdText != null)
            {
                motdText.text = cachedMotd;
            }
        }
    }

    public void RefreshMotd()
    {
        StartCoroutine(LoadMotdRoutine());
    }
}
