using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // The speed at which the player moves
    private Rigidbody2D rb; // The player's Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input for horizontal movement
        float horizontal = Input.GetAxis("Horizontal");
        // Get input for vertical movement
        float vertical = Input.GetAxis("Vertical");

        // Set the velocity of the Rigidbody2D component
        rb.velocity = new Vector2(horizontal, vertical) * speed;
    }
}