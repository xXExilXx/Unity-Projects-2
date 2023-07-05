using System;
using UnityEngine;

[AddComponentMenu("Dynamic Bone/Dynamic Bone Collider")]
public class DynamicBoneCollider : DynamicBoneColliderBase
{
    [Header("Collider Settings")]
    public float radius = 0.5f;
    public float height = 0f;

    private void OnValidate()
    {
        radius = Mathf.Max(radius, 0f);
        height = Mathf.Max(height, 0f);
    }

    public override void Collide(ref Vector3 particlePosition, float particleRadius)
    {
        float colliderRadius = radius * Mathf.Abs(base.transform.lossyScale.x);
        float halfHeight = height * 0.5f - radius;
        bool isSphere = halfHeight <= 0f;

        if (isSphere)
        {
            if (bound == DynamicBoneColliderBase.Bound.Outside)
            {
                OutsideSphere(ref particlePosition, particleRadius, transform.TransformPoint(center), colliderRadius);
            }
            else
            {
                InsideSphere(ref particlePosition, particleRadius, transform.TransformPoint(center), colliderRadius);
            }
        }
        else
        {
            Vector3 capsuleCenterA = center;
            Vector3 capsuleCenterB = center;

            switch (direction)
            {
                case DynamicBoneColliderBase.Direction.X:
                    capsuleCenterA.x -= halfHeight;
                    capsuleCenterB.x += halfHeight;
                    break;
                case DynamicBoneColliderBase.Direction.Y:
                    capsuleCenterA.y -= halfHeight;
                    capsuleCenterB.y += halfHeight;
                    break;
                case DynamicBoneColliderBase.Direction.Z:
                    capsuleCenterA.z -= halfHeight;
                    capsuleCenterB.z += halfHeight;
                    break;
            }

            if (bound == DynamicBoneColliderBase.Bound.Outside)
            {
                OutsideCapsule(ref particlePosition, particleRadius, transform.TransformPoint(capsuleCenterA), transform.TransformPoint(capsuleCenterB), colliderRadius);
            }
            else
            {
                InsideCapsule(ref particlePosition, particleRadius, transform.TransformPoint(capsuleCenterA), transform.TransformPoint(capsuleCenterB), colliderRadius);
            }
        }
    }

    private static void OutsideSphere(ref Vector3 particlePosition, float particleRadius, Vector3 sphereCenter, float sphereRadius)
    {
        float totalRadius = sphereRadius + particleRadius;
        float totalRadiusSquared = totalRadius * totalRadius;
        Vector3 offset = particlePosition - sphereCenter;
        float distanceSquared = offset.sqrMagnitude;

        if (distanceSquared > 0f && distanceSquared < totalRadiusSquared)
        {
            float distance = Mathf.Sqrt(distanceSquared);
            particlePosition = sphereCenter + offset * (totalRadius / distance);
        }
    }

    private static void InsideSphere(ref Vector3 particlePosition, float particleRadius, Vector3 sphereCenter, float sphereRadius)
    {
        float totalRadius = sphereRadius - particleRadius;
        float totalRadiusSquared = totalRadius * totalRadius;
        Vector3 offset = particlePosition - sphereCenter;
        float distanceSquared = offset.sqrMagnitude;

        if (distanceSquared > totalRadiusSquared)
        {
            float distance = Mathf.Sqrt(distanceSquared);
            particlePosition = sphereCenter + offset * (totalRadius / distance);
        }
    }

    private static void OutsideCapsule(ref Vector3 particlePosition, float particleRadius, Vector3 capsuleP0, Vector3 capsuleP1, float capsuleRadius)
    {
        float totalRadius = capsuleRadius + particleRadius;
        float totalRadiusSquared = totalRadius * totalRadius;
        Vector3 capsuleDirection = capsuleP1 - capsuleP0;
        Vector3 offset = particlePosition - capsuleP0;
        float dot = Vector3.Dot(offset, capsuleDirection);

        if (dot <= 0f)
        {
            float distanceSquared = offset.sqrMagnitude;

            if (distanceSquared > 0f && distanceSquared < totalRadiusSquared)
            {
                float distance = Mathf.Sqrt(distanceSquared);
                particlePosition = capsuleP0 + offset * (totalRadius / distance);
            }
        }
        else
        {
            float capsuleLengthSquared = capsuleDirection.sqrMagnitude;

            if (dot >= capsuleLengthSquared)
            {
                offset = particlePosition - capsuleP1;
                float distanceSquared = offset.sqrMagnitude;

                if (distanceSquared > 0f && distanceSquared < totalRadiusSquared)
                {
                    float distance = Mathf.Sqrt(distanceSquared);
                    particlePosition = capsuleP1 + offset * (totalRadius / distance);
                }
            }
            else
            {
                float projection = dot / capsuleLengthSquared;
                offset -= capsuleDirection * projection;
                float distanceSquared = offset.sqrMagnitude;

                if (distanceSquared > 0f && distanceSquared < totalRadiusSquared)
                {
                    float distance = Mathf.Sqrt(distanceSquared);
                    particlePosition += offset * ((totalRadius - distance) / distance);
                }
            }
        }
    }

    private static void InsideCapsule(ref Vector3 particlePosition, float particleRadius, Vector3 capsuleP0, Vector3 capsuleP1, float capsuleRadius)
    {
        float totalRadius = capsuleRadius - particleRadius;
        float totalRadiusSquared = totalRadius * totalRadius;
        Vector3 capsuleDirection = capsuleP1 - capsuleP0;
        Vector3 offset = particlePosition - capsuleP0;
        float dot = Vector3.Dot(offset, capsuleDirection);

        if (dot <= 0f)
        {
            float distanceSquared = offset.sqrMagnitude;

            if (distanceSquared > totalRadiusSquared)
            {
                float distance = Mathf.Sqrt(distanceSquared);
                particlePosition = capsuleP0 + offset * (totalRadius / distance);
            }
        }
        else
        {
            float capsuleLengthSquared = capsuleDirection.sqrMagnitude;

            if (dot >= capsuleLengthSquared)
            {
                offset = particlePosition - capsuleP1;
                float distanceSquared = offset.sqrMagnitude;

                if (distanceSquared > totalRadiusSquared)
                {
                    float distance = Mathf.Sqrt(distanceSquared);
                    particlePosition = capsuleP1 + offset * (totalRadius / distance);
                }
            }
            else
            {
                float projection = dot / capsuleLengthSquared;
                offset -= capsuleDirection * projection;
                float distanceSquared = offset.sqrMagnitude;

                if (distanceSquared > totalRadiusSquared)
                {
                    float distance = Mathf.Sqrt(distanceSquared);
                    particlePosition += offset * ((totalRadius - distance) / distance);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!base.enabled)
            return;

        Gizmos.color = (bound == DynamicBoneColliderBase.Bound.Outside) ? Color.yellow : Color.magenta;
        float colliderRadius = radius * Mathf.Abs(transform.lossyScale.x);
        float halfHeight = height * 0.5f - radius;
        bool isSphere = halfHeight <= 0f;

        if (isSphere)
        {
            Gizmos.DrawWireSphere(transform.TransformPoint(center), colliderRadius);
        }
        else
        {
            Vector3 capsuleCenterA = center;
            Vector3 capsuleCenterB = center;

            switch (direction)
            {
                case DynamicBoneColliderBase.Direction.X:
                    capsuleCenterA.x -= halfHeight;
                    capsuleCenterB.x += halfHeight;
                    break;
                case DynamicBoneColliderBase.Direction.Y:
                    capsuleCenterA.y -= halfHeight;
                    capsuleCenterB.y += halfHeight;
                    break;
                case DynamicBoneColliderBase.Direction.Z:
                    capsuleCenterA.z -= halfHeight;
                    capsuleCenterB.z += halfHeight;
                    break;
            }

            Gizmos.DrawWireSphere(transform.TransformPoint(capsuleCenterA), colliderRadius);
            Gizmos.DrawWireSphere(transform.TransformPoint(capsuleCenterB), colliderRadius);
            Gizmos.DrawLine(transform.TransformPoint(capsuleCenterA), transform.TransformPoint(capsuleCenterB));
        }
    }
}