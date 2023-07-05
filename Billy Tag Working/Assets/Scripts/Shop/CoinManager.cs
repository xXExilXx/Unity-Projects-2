using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private const int StartingCoins = 500;
    private const int DailyReward = 100;
    private const string LastRewardTimeKey = "LastRewardTime";
    private int _coins;

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

            // Calculate how many seconds have passed since the last reward
            long timeSinceLastReward = currentTime - lastRewardTime;

            // Calculate how many rewards the player has missed
            int missedRewards = (int)(timeSinceLastReward / (24 * 60 * 60)); // 24 hours in seconds

            // Give the player all the rewards they missed
            _coins = PlayerPrefs.GetInt("Coins", 0) + missedRewards * DailyReward;

            // Save the current time as the last reward time
            PlayerPrefs.SetInt(LastRewardTimeKey, (int)currentTime);
        }

        // Save the current coin balance
        PlayerPrefs.SetInt("Coins", _coins);
    }

    // Use this method to add or remove coins from the player's balance
    public void ModifyCoins(int amount)
    {
        _coins += amount;
        PlayerPrefs.SetInt("Coins", _coins);
    }

    // Use this method to get the current coin balance
    public int GetCoins()
    {
        return _coins;
    }
}
