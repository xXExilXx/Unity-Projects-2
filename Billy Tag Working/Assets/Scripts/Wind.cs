using UnityEngine;

public class Wind : MonoBehaviour
{
    public float boostForce = 1000f;
    public Vector3 shootingDirection = Vector3.forward;
    public Rigidbody rb;
    public CustomCollider.Collider cl;

    private void Awake()
    {
        CustomCollider.Collider.onetick = false;
        cl.tag = "Player";
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.CompareTag("Player") && !cl.istouched)
        {
            Rigidbody playerRigidbody = rb;
            playerRigidbody.AddForce(shootingDirection.normalized * boostForce, ForceMode.Impulse);
        }
    }
} 