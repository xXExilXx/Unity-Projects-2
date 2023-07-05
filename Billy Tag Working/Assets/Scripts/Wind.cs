using UnityEngine;

public class Wind : MonoBehaviour
{
    public float boostForce = 1000f; // Adjust the boost force as desired
    public Vector3 shootingDirection = Vector3.forward; // Set the shooting direction

    private bool waspressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable") && waspressed)
        {
            if(waspressed)
            {
                 waspressed = false;
            }
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            // Shoot the player in the specified direction immediately
            playerRigidbody.AddForce(shootingDirection.normalized * boostForce, ForceMode.Impulse);
        }
        waspressed = true;
    }
}
