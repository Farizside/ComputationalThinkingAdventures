using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private InputManager _input;
    [SerializeField] private float _movemenentSpeed;

    public float MovementSpeed
    {
        get => _movemenentSpeed;
        set => _movemenentSpeed = value;
    }

    private Animator _animator;
    private CharacterController _characterController;

    private int _isWalkingHash;
    private Vector3 _currentMovement;
    private bool _isMovementPressed;
    private Vector3 _faceTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        _isWalkingHash = Animator.StringToHash("isWalking");

        _input.MoveEvent += HandleMove;
    }

    private void OnDisable()
    {
        _input.MoveEvent -= HandleMove;
    }

    private void Update()
    {
        HandleAnimation();
        HandleGravity();
        Move();
    }

    private void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);

        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
    }

    private void HandleMove(Vector2 dir)
    {
        _currentMovement = new Vector3(dir.x, 0, dir.y);
        _isMovementPressed = dir.x != 0 || dir.y != 0;
    }

    private void Move()
    {
        if (_currentMovement == Vector3.zero)
        {
            return;
        }

        _characterController.Move(_currentMovement * (_movemenentSpeed * Time.deltaTime));
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            _currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            _currentMovement.y += gravity;
        }
    }
}