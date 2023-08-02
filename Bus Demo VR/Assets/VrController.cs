using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VrController : MonoBehaviour
{
    private Animator animator;
    public XRNode hand;
    public string gripParameterName = "Grip"; // Change this to match your grip parameter name in the Animator Controller
    public string triggerParameterName = "Trigger"; // Change this to match your trigger parameter name in the Animator Controller

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(hand);
        float gripValue = 0f;
        if (device.TryGetFeatureValue(CommonUsages.grip, out gripValue))
        {
            // Normalize gripValue to range [0, 1]
            gripValue = Mathf.Clamp01(gripValue);

            // Set the blend tree value for Grip parameter
            animator.SetFloat(gripParameterName, gripValue);
        }

        float triggerValue = 0f;
        if (device.TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
        {
            // Normalize triggerValue to range [0, 1]
            triggerValue = Mathf.Clamp01(triggerValue);

            // Set the blend tree value for Trigger parameter
            animator.SetFloat(triggerParameterName, triggerValue);
        }
    }
}
