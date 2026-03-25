using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("Settings")]
    public float sensitivity = 100f;
    public float verticalClamp = 60f;

    private float rotationX = 0f;

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // Đọc analog stick phải
        Vector2 look = gamepad.rightStick.ReadValue();

        // Xoay ngang (Y axis) — quay trái/phải
        transform.Rotate(Vector3.up * look.x * sensitivity * Time.deltaTime);

        // Xoay dọc (X axis) — nhìn lên/xuống, có giới hạn
        rotationX -= look.y * sensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -verticalClamp, verticalClamp);
        transform.localEulerAngles = new Vector3(
            rotationX,
            transform.localEulerAngles.y,
            0f
        );
    }
}