using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving = false;
    public Text distanceMoved;
    [SerializeField]
    private float Speed = 5.0f;
    public float distanceUnit = 0;
    private bool _isMoving;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 lastPosition;
    private float startTime; 
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    public void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
        _isMoving = (Vector2)transform.position == lastPosition;
        if (!_isMoving)
        {
            if (_movementInput.sqrMagnitude > 1)
            {
                _movementInput = _movementInput.normalized;
            }
            distance();
            distanceMoved.text = "Distance " + distanceUnit.ToString() + " meters";
        }
        lastPosition = transform.position;
    }

    private void MoveCharacter()
    {
       
        Vector2 velocity = _movementInput * Speed;
        _rigidbody.linearVelocity = velocity;
        _isMoving = (Vector2)transform.position == lastPosition;

        if (!_isMoving)
        {
            LookAt(velocity);
            distance();
            distanceMoved.text = "Distance " + distanceUnit.ToString() + " meters";
        }
        
        lastPosition = transform.position;
    }

    private void LookAt(Vector2 movementDirection)
    {
        if (movementDirection.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
    private void distance()
    {
        distanceUnit = distanceUnit + 1/Speed;
    }

}

