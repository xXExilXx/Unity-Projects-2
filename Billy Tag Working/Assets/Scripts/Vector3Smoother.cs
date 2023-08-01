using UnityEngine;

namespace Smoother
{
    public static class Vector3Smoother
    {
        // Variables to store the previous position for smoothing.
        private static Vector3 prevPosition;

        // Call this method to get the smoothed Vector3.
        public static Vector3 GetSmoothedVector3(Vector3 targetPosition, float smoothingFactor = 0.5f)
        {
            // Smooth the position using Lerp.
            Vector3 smoothedPosition = Vector3.Lerp(prevPosition, targetPosition, smoothingFactor);

            // Update the previous position for the next frame.
            prevPosition = smoothedPosition;

            return smoothedPosition;
        }
    }
}