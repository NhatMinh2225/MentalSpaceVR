using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("Look Settings")]
    public float lookSensitivity = 100f;
    public float verticalClamp  = 60f;

    [Header("Move Settings")]
    public float moveSpeed = 3f;
    public float gravity   = -9.8f;

    private float rotationX = 0f;
    private CharacterController controller;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // === NHÌN — Analog stick phải ===
        Vector2 look = gamepad.rightStick.ReadValue();
        transform.Rotate(Vector3.up * look.x * lookSensitivity * Time.deltaTime);
        rotationX -= look.y * lookSensitivity * Time.deltaTime;
        rotationX  = Mathf.Clamp(rotationX, -verticalClamp, verticalClamp);
        transform.localEulerAngles = new Vector3(
            rotationX,
            transform.localEulerAngles.y,
            0f
        );

        // === DI CHUYỂN — Analog stick trái ===
        Vector2 move = gamepad.leftStick.ReadValue();

        Vector3 forward = transform.forward;
        Vector3 right   = transform.right;
        forward.y = 0f;
        right.y   = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * move.y + right * move.x) * moveSpeed;

        // Gravity
        if (controller.isGrounded)
            verticalVelocity = -1f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        moveDir.y = verticalVelocity;

        controller.Move(moveDir * Time.deltaTime);
    }
}