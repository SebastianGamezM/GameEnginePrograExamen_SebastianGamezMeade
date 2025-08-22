using UnityEngine;
using UnityEngine.InputSystem.XInput;
public class PlayerCamera : MonoBehaviour
{
    private Transform padre;

    private float movX;
    private float movY;

    [SerializeField] private float MouseSenceX = 200f;
    [SerializeField] private float MouseSenceY = 200f;

    [SerializeField] private float smoothTime = 5f; 

    private float rotacionX = 0f;
    private float rotXVelocity = 0f;
    private float rotYVelocity = 0f;
    private float currentRotX = 0f;
    private float currentRotY = 0f;

    private void Start()
    {
        padre = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        movX = Input.GetAxis("Mouse X") * MouseSenceX * Time.deltaTime;
        movY = Input.GetAxis("Mouse Y") * MouseSenceY * Time.deltaTime;

        rotacionX -= movY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);


        currentRotX = Mathf.Lerp(currentRotX, rotacionX, smoothTime * Time.deltaTime);
        currentRotY = Mathf.Lerp(currentRotY, movX, smoothTime * Time.deltaTime);


        transform.localRotation = Quaternion.Euler(currentRotX, 0f, 0f);
        padre.Rotate(0f, currentRotY, 0f);
    }
}

