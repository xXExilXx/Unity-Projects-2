using UnityEngine;

public class NoclipCamera : MonoBehaviour
{
    public float speed = 10.0f;
    public float mouseSensitivity = 2.0f;
    public float zoomSensitivity = 2.0f;
    public float zoomSpeed = 10.0f;

    private float verticalLookRotation;
    private float currentZoom;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float zoom = Input.GetAxis("Mouse ScrollWheel");

        float movementSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed *= 2;
        }

        transform.position += transform.right * horizontal * movementSpeed * Time.deltaTime;
        transform.position += transform.forward * vertical * movementSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        verticalLookRotation += mouseY * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
        transform.localEulerAngles = new Vector3(-verticalLookRotation, transform.localEulerAngles.y, 0);

        currentZoom = Mathf.Clamp(currentZoom - zoom * zoomSensitivity, -50, 50);
        transform.position += transform.forward * currentZoom * zoomSpeed * Time.deltaTime;
    }
}