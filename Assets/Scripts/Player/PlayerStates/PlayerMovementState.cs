using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;

    private float _horizontalMovement;
    private float _verticalDirection;
    private float _movement;
    private int _idleAnimatorParameter = Animator.StringToHash("Idle");
    private int _runAnimatorParameter = Animator.StringToHash("Run");


    public override void ExecuteState()
    {
        MovePlayer();
    }

    private void MovePlayer() 
    {
        if (Mathf.Abs(_horizontalInput) > 0.1f) // If horizontal input is more than 0.1f then we're moving our player
        {
            _movement = _horizontalMovement;
        }

        else
        {
            _movement = 0f;
        }

        float moveSpeed = _movement * speed;
        _playerController.SetHorizontalForce(moveSpeed); // Passing moveSpeed to _force.x
    }

    protected override void GetInput()
    {
        _horizontalMovement = _horizontalInput;
        _verticalDirection = _verticalInput;
    }

    public override void SetAnimation()
    {
        _animator.SetBool(_idleAnimatorParameter, _horizontalMovement == 0 
                            && _playerController.Conditions.isCollidingBelow
                            && !_playerController.Conditions.isShootingAndRunningForward
                            && !_playerController.Conditions.isShootingAndRunningUpward);
        _animator.SetBool(_runAnimatorParameter, Mathf.Abs(_horizontalInput) > 0.1f 
                            && _playerController.Conditions.isCollidingBelow
                            && !Input.GetMouseButton(0)
                            && !_playerController.Conditions.isShootingAndRunningForward
                            && !_playerController.Conditions.isShootingAndRunningUpward);
    }
}
