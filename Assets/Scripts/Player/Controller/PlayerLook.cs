using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    float xRotation = 0f;

    // float _mouseX;
    // float _mouseY;

    Vector2 _mouseDirection;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float MouseX = _mouseDirection.x * mouseSensitivity * Time.deltaTime;
        float MouseY = _mouseDirection.y * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * MouseX);
    }

    public void Look_Action(InputAction.CallbackContext context) {
        _mouseDirection = context.ReadValue<Vector2>();
    }
}
