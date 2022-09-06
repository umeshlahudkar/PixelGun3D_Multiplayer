using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float lookSensitivity;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform mainCamera;

    private float forwardInput = 0;
    private float rotationInput = 0;
    private float lookUpORDownInput = 0;
    private float currentLookAngle = 0;

    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical") * movementSpeed;
        rotationInput = Input.GetAxis("Mouse X") * rotationSpeed;
        lookUpORDownInput = Input.GetAxis("Mouse Y") * lookSensitivity;
    }

    private void FixedUpdate()
    {
        Rotate();

        if (forwardInput != 0)
        {
            MoveForward();
        }

        if (lookUpORDownInput != 0)
        {
            LookUpAndDown();
        }
    }

    private void MoveForward()
    {
        rb.MovePosition(rb.position + transform.forward * forwardInput * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotationInput * Time.fixedDeltaTime, 0));
    }

    private void LookUpAndDown()
    {
        currentLookAngle -= lookUpORDownInput;
        currentLookAngle = Mathf.Clamp(currentLookAngle, -30, 10);
        mainCamera.localRotation = Quaternion.Euler(currentLookAngle,0,0);
    }
}
