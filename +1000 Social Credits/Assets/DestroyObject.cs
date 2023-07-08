using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float duration;

    void Start()
    {
        Invoke("Destroy", duration);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}