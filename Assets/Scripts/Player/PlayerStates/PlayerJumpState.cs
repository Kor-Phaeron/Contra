using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 1;

    private int _jumpAnimatorParameter = Animator.StringToHash("Jump");
    private int _animatorDoubleJumpParameter = Animator.StringToHash("Double Jump");
    private int _animatorFallParameter = Animator.StringToHash("Fall");


    public int JumpsLeft { get; set; }

    protected override void InitState()
    {
        base.InitState();
        JumpsLeft = maxJumps;
    }

    public override void ExecuteState()
    {
        if (_playerController.Conditions.isCollidingBelow && _playerController.Force.y == 0)
        {
            JumpsLeft = maxJumps;
            _playerController.Conditions.isJumping = false;
        }
    }

    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!CanJump())
        {
            return;
        }
        if (JumpsLeft == 0)
        {
            return;
        }
        JumpsLeft -= 1;

        float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(_playerController.Gravity));
        _playerController.SetVerticalForce(jumpForce);
        _playerController.Conditions.isShootingAndRunningForward = false;
        _playerController.Conditions.isShootingAndRunningUpward = false;
        _playerController.Conditions.isJumping = true;
    }

    private bool CanJump()
    {
        if (!_playerController.Conditions.isCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }

        if (_playerController.Conditions.isCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }


        JumpsLeft = maxJumps;
        return true;
    }

    public override void SetAnimation()
    {
        // Jump
        _animator.SetBool(_jumpAnimatorParameter, _playerController.Conditions.isJumping
                        && !_playerController.Conditions.isCollidingBelow
                        && JumpsLeft == 0
                        && !_playerController.Conditions.isFalling);
        // Double jump
/*        _animator.SetBool(_animatorDoubleJumpParameter, _playerController.Conditions.isJumping
                        && !_playerController.Conditions.isCollidingBelow
                        && JumpsLeft == 0
                        && !_playerController.Conditions.isFalling);*/
        // Fall
        /*_animator.SetBool(_animatorFallParameter, _playerController.Conditions.isFalling
                        && _playerController.Conditions.isJumping
                        && !_playerController.Conditions.isCollidingBelow);*/
    }
}
