using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public float shootForce;

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the direction and distance from the shooter to the mouse position
            Vector2 shootDir = (Vector2)(mousePos - transform.position);
            float shootDist = shootDir.magnitude;
            shootDir.Normalize();

            // Create a ball at the shooter's position
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            // Add force to the ball in the direction of the mouse position
            ball.GetComponent<Rigidbody2D>().AddForce(shootDir * shootForce);
        }
    }

}
