using UnityEngine;

public class ScoreOnTouch : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameObject Object;


    private void Start()
    {
        // Load the high score from PlayerPrefs
        ScoreManager.highScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            // Add points to the score
            ScoreManager.score += 1000;

            // Update the high score if the current score is higher
            if (ScoreManager.score > ScoreManager.highScore)
            {
                ScoreManager.highScore = ScoreManager.score;
                PlayerPrefs.SetFloat("HighScore", ScoreManager.highScore);
            }

            Destroy(Object);
        }
    }
}
