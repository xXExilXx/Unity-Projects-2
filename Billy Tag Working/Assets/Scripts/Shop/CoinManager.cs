using System;
using UnityEngine;
using UnityEditor;

public class CoinManager : MonoBehaviour
{
    public int Coins;
    public bool Reset;

    public int addCoins;
    public bool enter;
    private const int StartingCoins = 500;
    private const int DailyReward = 100;
    private const string LastRewardTimeKey = "LastRewardTime";
    private int _coins;

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void Start()
    {
        // Check if this is the first time the player is starting the game
        if (!PlayerPrefs.HasKey("HasStartedBefore"))
        {
            // First time playing, give them starting coins
            _coins = StartingCoins;
            PlayerPrefs.SetInt("HasStartedBefore", 1);
        }
        else
        {
            // Not the first time playing, check when the last reward was given
            long lastRewardTime = PlayerPrefs.GetInt(LastRewardTimeKey, 0);
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // Calculate the time at which the current day started
            long currentDayStartTime = currentTime - (currentTime % (24 * 60 * 60)); // 24 hours in seconds

            // Calculate how many seconds have passed since the last reward
            long timeSinceLastReward = currentDayStartTime - lastRewardTime;

            if (timeSinceLastReward >= (24 * 60 * 60))
            {
                // The player is eligible for the current day's reward
                _coins = PlayerPrefs.GetInt("Coins", 0) + DailyReward;
                // Save the current time as the last reward time
                PlayerPrefs.SetInt(LastRewardTimeKey, (int)currentDayStartTime);
            }
            else
            {
                // The player is not eligible for today's reward, so just get the current coin balance
                _coins = PlayerPrefs.GetInt("Coins", 0);
            }
        }

        // Save the current coin balance
        PlayerPrefs.SetInt("Coins", _coins);
        Coins = GetCoins();
    }

    public void Update()
    {
        if (Reset)
        {
            PlayerPrefs.DeleteKey("HasStartedBefore");
            PlayerPrefs.DeleteKey("Coins");
            _coins = StartingCoins;
            PlayerPrefs.SetInt("HasStartedBefore", 1);
            PlayerPrefs.SetInt("Coins", _coins);
            Coins = GetCoins();
            Reset = false;
        }
        if (enter)
        {
            if(addCoins == 0)
            {
                Debug.Log("You have to enter a value");
                enter = false;
            }
            else
            {
                _coins += addCoins;
                PlayerPrefs.SetInt("Coins", _coins);
                Coins = GetCoins();
                enter = false;
            }
        }
    }

    // Use this method to add or remove coins from the player's balance
    public void ModifyCoins(int amount)
    {
        _coins -= amount;
        PlayerPrefs.SetInt("Coins", _coins);
    }

    // Use this method to get the current coin balance
    public int GetCoins()
    {
        return _coins;
    }
    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            // Code to execute when exiting Play Mode
            PlayerPrefs.Save();
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
