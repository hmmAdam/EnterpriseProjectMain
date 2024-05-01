using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerInput inputActions;
    InputAction move, look, jump, fire;

    CharacterController characterController;
    public Transform cameraContainer;

    public float maxSpeed = 10f;
    public float jumpSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float gravity = 20.0f;

    Vector3 moveDirection = Vector3.zero;
    float rotateX, rotateY;

    float speed = 6f;
    float lookUpClamp = -5f;
    float lookDownClamp = 20f;
    float rotateYaw, rotatePitch;
    bool lift = false;

    private void Awake()
    {
        inputActions = new PlayerInput();
    }

    void Start()
    {
        UnityEngine.Cursor.visible = false;
        characterController = GetComponent<CharacterController>();

        inputActions.Player.Move.Enable();
        inputActions.Player.Look.Enable();
        inputActions.Player.Jump.Enable();
        inputActions.Player.Fire.Enable();
    }

    void Update()
    {
        RotateAndLook();
    }

    void FixedUpdate()
    {
        if (move.ReadValue<Vector2>() != Vector2.zero)
        {
            Debug.Log("Move: " + move.ReadValue<Vector2>());
        }
        if (look.ReadValue<Vector2>() != Vector2.zero)
        {
            Debug.Log("Look: " + look.ReadValue<Vector2>());
        }

        Locomotion();
    }

    private void OnEnable()
    {
        move = inputActions.Player.Move;
        move.Enable();

        look = inputActions.Player.Look;
        look.Enable();

        jump = inputActions.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump Button Pressed");
        lift = true;
    }

    void Locomotion()
    {
        if (characterController.isGrounded) // When grounded, set y-axis to zero (to ignore it)
        {
            moveDirection = new Vector3(move.ReadValue<Vector2>().x, 0f, move.ReadValue<Vector2>().y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (lift)
            {
                lift = false;
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void RotateAndLook()
    {
        rotateX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotateY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotateY = Mathf.Clamp(rotateY, lookUpClamp, lookDownClamp);

        transform.Rotate(0f, rotateX, 0f);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);
    }
}
