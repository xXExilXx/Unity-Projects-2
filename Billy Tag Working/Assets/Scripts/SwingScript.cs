using UnityEngine;
using UnityEngine.XR;
using Smoother;

public class SwingScript : MonoBehaviour
{
    public XRNode controllerNode;
    public LayerMask whatIsGrappleable;
    public float maxDistance = 100f;
    public Transform Player, Hands;
    public GameObject AimPrefab;

    private GameObject aimball;
    private LineRenderer lr;
    private Vector3 grapplePoint, currentGrapplePosition;
    private SpringJoint joint;
    private bool triggerButton, gripButton, wasPressed, wasPressedGrip;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        aimball = Instantiate(AimPrefab);
        lr.enabled = false;
    }
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButton);

        if (triggerButton && !wasPressed)
        {
            StartGrapple();
            Destroy(aimball);
            wasPressed = true;
        }
        else if (!triggerButton && wasPressed)
        {
            StopGrapple();
            wasPressed = false;
        }

        device.TryGetFeatureValue(CommonUsages.gripButton, out gripButton);

        if (gripButton && !wasPressedGrip)
        {
            if (joint != null)
            {
                joint.maxDistance = 0f;
                joint.minDistance = 0.1f;
            }
            wasPressedGrip = true;
        }
        else if (!gripButton && wasPressedGrip)
        {
            wasPressedGrip = false;
        }
    }
    void LateUpdate()
    {
        DrawRope();

        RaycastHit hit;

        if (Physics.Raycast(Hands.position, Hands.forward, out hit, maxDistance, whatIsGrappleable))
        {
            if (aimball != null)
            {
                aimball.transform.position = Vector3Smoother.GetSmoothedVector3(hit.point, 5f);
            }
            else
            {
                if (!wasPressed)
                {
                    aimball = Instantiate(AimPrefab);
                }
            }
        }
        else
        {
            Destroy(aimball);
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(Hands.position, Hands.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = Vector3Smoother.GetSmoothedVector3(hit.point, 5f);
            lr.enabled = true;
            joint = Player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(Hands.position, grapplePoint);
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
            joint.spring = 15f;
            joint.damper = 10f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = Hands.position;
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        lr.enabled = false;
    }

    void DrawRope()
    {
        if (!joint)
            return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, currentGrapplePosition);
    }
}