using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;

    [SerializeField] private float _gravity = -28f;

    [Header("Speed Settings")]
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _stamina = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpMaxHeight = 3.5f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _ceilingCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private float _ceilingDistance = 0.4f;
    
    [SerializeField] private LayerMask groundMask;

    Vector3 _moveVelocity;

    Vector3 _gravityVelocity;
    bool _isGrounded;
    bool _isTouchingCeiling;

    bool _isSprinting;

    void Update() {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, groundMask);
        _isTouchingCeiling = Physics.CheckSphere(_ceilingCheck.position, _ceilingDistance, groundMask);

        Gravity();
        Movement();
    }

    private void Gravity() {
        if(_isGrounded && _gravityVelocity.y < 0)
        {
            _gravityVelocity.y = -2f;
        }

        _gravityVelocity.y += _gravity * Time.deltaTime;

        _controller.Move(_gravityVelocity * Time.deltaTime);

        if (_isTouchingCeiling && _gravityVelocity.y > 0) {
            _gravityVelocity.y = -2f;
        }
    }

    private void Movement() {
        Vector3 move = transform.right * _moveVelocity.x + transform.forward * _moveVelocity.z;

        if (_isSprinting && _moveVelocity.z > 0.7f && _isGrounded && _stamina > 0.5f) {
            _controller.Move(move * _sprintSpeed * Time.deltaTime);
            _stamina -= Time.deltaTime;

            if (_stamina < 0.5f) {
                _stamina = -0.5f;
            }
        } else {
            _controller.Move(move * _speed * Time.deltaTime);

            if (_stamina < 5f) {
                _stamina += Time.deltaTime;
            }
        }
    }



    private void Jump() {
        if (_isGrounded) {
            _gravityVelocity.y = Mathf.Sqrt(_jumpMaxHeight * -2f * _gravity);
        }
    }

    public void Move_Action(InputAction.CallbackContext context) {
        _moveVelocity.x = context.ReadValue<Vector2>().x;
        _moveVelocity.z = context.ReadValue<Vector2>().y;
    }

    public void Sprint_Action(InputAction.CallbackContext context) {
        if (context.performed) {
            _isSprinting = true;
            Debug.Log("Verdadero");
        } else {
            _isSprinting = false;
            Debug.Log("Falso");
        }
    }

    public void Jump_Action(InputAction.CallbackContext context) {
        if (context.started) {
            Jump();
        } 
    }
}
