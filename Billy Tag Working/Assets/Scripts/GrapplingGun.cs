using UnityEngine;
using UnityEngine.XR;

public class GrapplingGun : MonoBehaviour
{
    public Transform tip;
    public float maxDistance = 100f;
    public float grapplingForce = 10f;
    public XRNode Controller;

    private LineRenderer lineRenderer;
    private XRNode inputSource;
    private bool isGrappling = false;
    private Vector3 grapplingPoint;
    private Rigidbody connectedRigidbody;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set the input source to the right hand
        inputSource = Controller;
    }

    void Update()
    {
        // Check for input from the specified XRNode
        InputDevice inputDevice = InputDevices.GetDeviceAtXRNode(inputSource);
        bool triggerValue;
        inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue);
        if (triggerValue)
        {
            if (!isGrappling)
                StartGrapple();
        }
        else
        {
            if (isGrappling)
                StopGrapple();
        }

        if (isGrappling)
        {
            // Update the line renderer positions
            lineRenderer.SetPosition(0, tip.position);
            lineRenderer.SetPosition(1, grapplingPoint);

            // Apply force to the connected rigidbody
            connectedRigidbody.AddForce((grapplingPoint - tip.position) * grapplingForce);
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(tip.position, tip.forward, out hit, maxDistance))
        {
            grapplingPoint = hit.point;
            connectedRigidbody = hit.rigidbody;

            // Enable the line renderer and set its positions
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, tip.position);
            lineRenderer.SetPosition(1, grapplingPoint);

            isGrappling = true;
        }
    }

    void StopGrapple()
    {
        // Disable the line renderer
        lineRenderer.enabled = false;
        isGrappling = false;
    }
}
