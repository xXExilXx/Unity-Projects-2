using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int currentScore;

    private void Start()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddScore(int scoreValue)
    {
        currentScore += scoreValue;
        UpdateScoreText();
    }
}
