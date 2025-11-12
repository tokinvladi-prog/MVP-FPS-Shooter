using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset InputActions;
    public Transform Camera;

    private InputAction _moveAction;
    private InputAction _lookAction;

    private Vector2 _moveAmt;
    private Vector2 _lookAmt;
    private Rigidbody _rb;

    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _sensitivity = 1f;
    private float _rotateY = 0;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();

        _rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        _moveAmt = _moveAction.ReadValue<Vector2>();
        _lookAmt = _lookAction.ReadValue<Vector2>();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new(_moveAmt.x, 0, _moveAmt.y);

        _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * transform.TransformDirection(movement));
    }

    private void Look()
    {
        _rotateY -= _lookAmt.y * _sensitivity;
        _rotateY = Mathf.Clamp(_rotateY, -90f, 90f);
        Camera.localRotation = Quaternion.Euler(new Vector3(_rotateY, 0, 0));

        transform.Rotate(_lookAmt.x * _sensitivity * Vector3.up);
    }
}
