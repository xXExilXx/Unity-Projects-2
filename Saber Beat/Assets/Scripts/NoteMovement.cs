using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private float speed;

    public void SetSpeed(float noteSpeed)
    {
        speed = noteSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
