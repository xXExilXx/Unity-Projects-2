using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    public int damageAmount = 10;
    public int NewHealth;
    public Slider healthSlider;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage()
    {
        // Decrease health by the damage amount
        currentHealth -= damageAmount;

        // Clamp the health within the valid range
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar display
        UpdateHealthBar();

        // Check if the player is defeated
        if (currentHealth <= 0)
        {
            // Handle player defeat here
            PlayerDefeated();
        }
    }


    public void BlockSuccessful()
    {
        // Increase health by a certain amount for a successful block
        currentHealth += NewHealth;

        // Clamp the health within the valid range
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
 
        // Update the health bar display
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Calculate the fill amount for the health bar
        float fillAmount = (float)currentHealth / maxHealth;

        // Update the health bar slider value
        healthSlider.value = fillAmount;
    }

    private void PlayerDefeated()
    {
        // Handle player defeat here
        // You can restart the game, show a game over screen, etc.
        Debug.Log("Player Defeated");
    }
}
