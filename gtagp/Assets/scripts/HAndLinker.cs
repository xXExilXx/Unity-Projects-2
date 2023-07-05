using UnityEngine;

public class HAndLinker : MonoBehaviour
{
    [Header("Hands")]
    public Transform HandL;
    public Transform HandR;
    public Transform TargetR;
    public Transform TargetL;
    [Space]
    [Header("Head")]
    public Transform Head;
    public Transform HeadFollower;
    public Transform Camera;
    [Space]
    [Space]
    public Vector3 CameraOffset;
    public Vector3 HeadOffset;


    void Awake()
    {
        Camera.rotation = Quaternion.Euler(CameraOffset.x, CameraOffset.y, CameraOffset.z);
        HeadFollower.rotation = Quaternion.Euler(HeadOffset.x, HeadOffset.y, HeadOffset.z);
    }

    void Update()
    {
        TargetL.position = HandL.position;
        TargetL.rotation = HandL.rotation;
        TargetR.position = HandR.position;
        TargetR.rotation = HandR.rotation;
        Camera.position = Head.position;
    }
}
