using System;
using UnityEngine;

public class DynamicBoneColliderBase : MonoBehaviour
{
    public virtual void Collide(ref Vector3 particlePosition, float particleRadius)
    {
    }

    public Direction direction = Direction.Y;
    public Vector3 center = Vector3.zero;
    public Bound bound = Bound.Outside;

    public enum Direction
    {
        X,
        Y,
        Z
    }

    public enum Bound
    {
        Outside,
        Inside
    }
}
