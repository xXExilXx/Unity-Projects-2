using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    public Transform target; // The object to follow
    public Collider boundsCollider; // The collider that defines the movement bounds

    private void LateUpdate()
    {
        if (target == null || boundsCollider == null)
        {
            return;
        }

        // Get the target's position in the local space of the collider
        Vector3 targetLocalPosition = boundsCollider.transform.InverseTransformPoint(target.position);

        // Get the current position of this object in the local space of the collider
        Vector3 currentPosition = boundsCollider.transform.InverseTransformPoint(transform.position);

        // Clamp the local X and Y positions of the target within the collider's bounds
        float clampedX = Mathf.Clamp(targetLocalPosition.x, boundsCollider.bounds.min.x, boundsCollider.bounds.max.x);
        float clampedY = Mathf.Clamp(targetLocalPosition.y, boundsCollider.bounds.min.y, boundsCollider.bounds.max.y);

        // Calculate the difference in X and Y position between this object and the target
        float deltaX = clampedX - targetLocalPosition.x;
        float deltaY = clampedY - targetLocalPosition.y;

        // Calculate the new local position of this object based on the target's position and clamped values
        Vector3 newLocalPosition = new Vector3(currentPosition.x - deltaX, currentPosition.y - deltaY, 0f);

        // Move this object instantly to the new local position without any interpolation
        transform.position = boundsCollider.transform.TransformPoint(newLocalPosition);
    }
}
