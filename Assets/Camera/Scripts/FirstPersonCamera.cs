using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerBody; // Reference to the player model's Transform
    public float mouseSensitivity = 100f;

    float xAxisClamp = 0.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp -= mouseY;

        Vector3 targetRotation = transform.rotation.eulerAngles;
        targetRotation.x -= mouseY;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Euler(targetRotation);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
