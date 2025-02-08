using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving => _isMoving;

    [SerializeField]
    private float Speed = 5.0f;

    private bool _isMoving;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput = Vector2.zero;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    public void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();

        
        if (_movementInput.sqrMagnitude > 1)
        {
            _movementInput = _movementInput.normalized;
        }
    }

    private void MoveCharacter()
    {
        
        Vector2 velocity = _movementInput * Speed;
        _rigidbody.linearVelocity = velocity;
        _isMoving = velocity.sqrMagnitude > 0.01f;

        if (_isMoving)
        {
            LookAt(velocity);
        }
    }

    private void LookAt(Vector2 movementDirection)
    {
        if (movementDirection.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
