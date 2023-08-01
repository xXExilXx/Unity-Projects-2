using UnityEngine;
using UnityEngine.XR;
public class SwingScript : MonoBehaviour {

    public XRNode controllerNode;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform Hand, Player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    public GameObject AimPrefab;
    public bool primaryTriggerValue;
    bool waspressed;
    GameObject aimball;

    void Awake() {
        lr = GetComponent<LineRenderer>();
        aimball = Instantiate(AimPrefab);
    }

    void Update() {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out primaryTriggerValue);
        if(primaryTriggerValue && !waspressed)
        {
            StartGrapple();
            Destroy(aimball);
            waspressed = true;
        }
        else if(!primaryTriggerValue && waspressed)
        {
            StopGrapple();
            waspressed = false;
        }
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
        RaycastHit hit;
        if(Physics.Raycast(Hand.position, Hand.forward, out hit, maxDistance, whatIsGrappleable))
        {
            if(aimball != null)
            {

                aimball.transform.position = hit.point;
            }
            else
            {
                if(!waspressed)
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
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(Hand.position, Hand.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = Player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            float distanceFromPoint = Vector3.Distance(Hand.position, grapplePoint);
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
            joint.spring = 15f;
            joint.damper = 10f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = Hand.position;
        }
    }
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, Hand.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}