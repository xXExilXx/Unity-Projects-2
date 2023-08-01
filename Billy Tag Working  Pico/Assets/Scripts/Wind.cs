using UnityEngine;

public class Wind : MonoBehaviour
{
    public float boostForce = 1000f; // Adjust the boost force as desired
    public Vector3 shootingDirection = Vector3.forward; // Set the shooting direction
    public Rigidbody rb;
    private bool triggeredOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && triggeredOnce)
        {
            Rigidbody playerRigidbody = rb;
            // Shoot the player in the specified direction immediately
            playerRigidbody.AddForce(shootingDirection.normalized * boostForce, ForceMode.Impulse);
        }
        else
        {
            // Mark the trigger as triggered once
            triggeredOnce = true;
        }
    }
}
