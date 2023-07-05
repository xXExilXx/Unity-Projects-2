using UnityEngine;

public class Reset : MonoBehaviour
{
    public Transform Target;

    void Awake()
    {
        transform.position = Target.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = Target.position;
        }
    }
}