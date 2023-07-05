using System;
using UnityEngine;

public class physHands : MonoBehaviour
{
    [Header("PID")]
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] float frequency = 50f;
    [SerializeField] float damping = 1f;
    [SerializeField] float rotFrequency = 100f;
    [SerializeField] float rotDamping = 0.9f;
    [Space]
    [Header("Controller")]
    [SerializeField] Transform target;
    [Space]
    [Header("Springs")]
    [SerializeField] float climbDrag = 500f;
    [SerializeField] float climbForce = 1000f;

    Rigidbody _rigidbody;
    Vector3 _previousPosition;
    bool _isTouching;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = float.PositiveInfinity;
        _previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        PIDMovement();
        PIDRotation();
        //the old one works too but this kinda looks better this way
        if (_isTouching)
        {
            HookesLaw();
        }
    }

    void HookesLaw()
    {
        Vector3 displacementFromResting = transform.position - target.position;
        Vector3 force = displacementFromResting * climbForce;
        float drag = GetDrag();
        playerRigidbody.AddForce(force, ForceMode.Acceleration);
        playerRigidbody.AddForce(drag * -playerRigidbody.velocity * climbDrag);
    }

    void PIDRotation()
    {
        float kp = (6f * rotFrequency) * (6f * rotFrequency) * 0.25f;
        float kd = 4.5f * rotFrequency * rotDamping;
        float g = 1 / (1 + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Quaternion q = target.rotation * Quaternion.Inverse(transform.rotation);
        if (q.w < 0)
        {
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        q.ToAngleAxis(out float angle, out Vector3 axis);
        axis.Normalize();
        axis *= Mathf.Deg2Rad;
        Vector3 torque = ksg * axis * angle + -_rigidbody.angularVelocity * kdg;
    }

    void PIDMovement()
    {
        float kp = (6f * frequency) * (6f * frequency) * 0.25f;
        float kd = 4.5f * frequency * damping;
        float g = 1 / (1 + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Vector3 force = (target.position - transform.position) * ksg + (playerRigidbody.velocity - _rigidbody.velocity) * kdg;
        _rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    float GetDrag()
    {
        Vector3 handVelocity = (target.localPosition - _previousPosition) / Time.fixedDeltaTime;
        float drag = 1 / handVelocity.magnitude + 0.01f;
        drag = drag > 1 ? 1 : drag;
        drag = drag < 0.03f ? 0.03f : drag;
        _previousPosition = transform.position;
        return drag;
    }

    void OnCollisionEnter(Collision collision)
    {
        _isTouching = true;
        Debug.Log("Hooks law active");
    }

    void OnCollisionExit(Collision collision)
    {
        _isTouching = false;
        Debug.Log("Hooks law unactive");
    }
}

