using UnityEngine;

public class RotateTransforms : MonoBehaviour
{
    public Transform[] objectsToRotate; // Array of objects to rotate
    public float rotationSpeed = 1f; // Rotation speed for all objects

    private void Update()
    {
        for (int i = 0; i < objectsToRotate.Length; i++)
        {
            float objectSpeed = rotationSpeed * (i + 1); // Calculate speed offset for each object

            // Get the local rotation axes of the object
            Vector3 localRotationAxis = objectsToRotate[i].transform.TransformDirection(Vector3.right);

            // Calculate the rotation amount based on the object's scale
            float rotationAmount = objectSpeed * Time.deltaTime / objectsToRotate[i].transform.lossyScale.magnitude;

            // Rotate the object around the local rotation axis
            objectsToRotate[i].transform.localRotation *= Quaternion.AngleAxis(rotationAmount, localRotationAxis);
        }
    }
}
