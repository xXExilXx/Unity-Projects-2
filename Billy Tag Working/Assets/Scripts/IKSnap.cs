using UnityEngine;

public class IKSnap : MonoBehaviour
{
    public Transform leftHandController;
    public Transform leftHand;
    public Vector3 leftHandOffset;
    public Transform rightHandController;
    public Transform rightHand;
    public Vector3 rightHandOffset;
    public Transform VRHead;
    public Transform head;
    public Vector3 headOffset;
    public Vector3 headRotationOffset; // the new variable to store the rotation offset

    private void LateUpdate()
    {
        if (leftHand != null)
        {
            leftHandController.position = leftHand.position + leftHandOffset;
            leftHandController.rotation = leftHand.rotation;
        }
        if (rightHand != null)
        {
            rightHandController.position = rightHand.position + rightHandOffset;
            rightHandController.rotation = rightHand.rotation;
        }
        if (head != null)
        {
            VRHead.position = head.position + headOffset;
            VRHead.rotation = head.rotation * Quaternion.Euler(headRotationOffset);
        }
    }
}
