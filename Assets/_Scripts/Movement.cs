using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _rotationSpeed = 720f;
    Rigidbody2D _rigidbody;
    Vector2 _movementInput;
    Vector2 _smoothedMovementInput;
    Vector2 _movementInputSmoothVelocity;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f
                );
        _rigidbody.velocity = _smoothedMovementInput * _speed;
    }

    void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }

    void OnMove(InputValue inputValue)
    {
       _movementInput = inputValue.Get<Vector2>();
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionExit2D(Collision2D other) {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
    }

    public void MoveInY(int amount)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + amount);
    }
}
