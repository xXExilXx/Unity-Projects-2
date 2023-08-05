using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public float mass = 1f;
    public bool useGravity = true;
    public float gravityScale = 1f;
    public Vector3 velocity;
    public Vector3 angularVelocity;

    public bool freezePositionX = false;
    public bool freezePositionY = false;
    public bool freezePositionZ = false;
    public bool freezeRotationX = false;
    public bool freezeRotationY = false;
    public bool freezeRotationZ = false;

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public Vector3 AngularVelocity
    {
        get { return angularVelocity; }
        set { angularVelocity = value; }
    }

    void Update()
    {
        // Apply gravity
        if (useGravity)
            velocity += Physics.gravity * gravityScale * Time.deltaTime;

        // Integrate position and rotation
        Vector3 newPos = transform.position + velocity * Time.deltaTime;
        transform.position = new Vector3(
            freezePositionX ? transform.position.x : newPos.x,
            freezePositionY ? transform.position.y : newPos.y,
            freezePositionZ ? transform.position.z : newPos.z
        );

        Vector3 newRotation = angularVelocity * Time.deltaTime;
        if (!freezeRotationX) transform.Rotate(newRotation.x, 0f, 0f);
        if (!freezeRotationY) transform.Rotate(0f, newRotation.y, 0f);
        if (!freezeRotationZ) transform.Rotate(0f, 0f, newRotation.z);
    }

    public void AddForce(Vector3 force)
    {
        velocity += force / mass;
    }

    public void AddTorque(Vector3 torque)
    {
        angularVelocity += torque / mass;
    }
}
