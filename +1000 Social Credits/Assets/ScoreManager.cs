using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static float highScore;
    public bool reset;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoretext;

    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    void Update()
    {
        if (reset)
        {
            PlayerPrefs.DeleteKey("HighScore");
            reset = false;
        }
        scoreText.text = "Social Credit's: " + score;
        highscoretext.text = " High Score: " + highScore;
    }
}