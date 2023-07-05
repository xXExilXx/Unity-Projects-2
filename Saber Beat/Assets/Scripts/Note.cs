using UnityEngine;

public enum NoteColorType
{
    Red,
    Blue
}

public class Note : MonoBehaviour
{
    public int maxScoreValue = 115;
    public int minScoreValue = 10;
    public float perfectHitRange = 0.1f;
    public float goodHitRange = 0.2f;
    public int maxComboMultiplier = 5;

    public NoteColorType colorType;

    private bool hasBeenHit = false;
    private int comboCount = 0;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Saber") && !hasBeenHit)
        {
            hasBeenHit = true;

            float distance = Vector3.Distance(collider.transform.position, transform.position);
            int hitScore = CalculateHitScore(distance);

            comboCount++;

            hitScore *= comboCount;

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(hitScore);
            }
            HealthBar health = FindObjectOfType<HealthBar>();
            if(health != null){
                health.BlockSuccessful();
            }
        }
    }

    private int CalculateHitScore(float distance)
    {
        if (distance <= perfectHitRange)
        {
            return maxScoreValue;
        }
        else if (distance <= goodHitRange)
        {
            return (int)(maxScoreValue * 0.5f);
        }
        else
        {
            return minScoreValue;
        }
    }
}
