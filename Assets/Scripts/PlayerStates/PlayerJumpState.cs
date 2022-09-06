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

    private Vector2 _currentColliderSize;
    private Vector2 _jumpingColliderSize;


    public int JumpsLeft { get; set; }

    protected override void InitState()
    {
        base.InitState();
        JumpsLeft = maxJumps;
        _currentColliderSize = _playerController.GetComponent<BoxCollider2D>().size;
        
    }

    public override void ExecuteState()
    {
        if (_playerController.Conditions.isCollidingBelow && _playerController.Force.y == 0)
        {
            JumpsLeft = maxJumps;
            _playerController.Conditions.isJumping = false;
            _playerController.GetComponent<BoxCollider2D>().size = _currentColliderSize;
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
        _playerController.Conditions.isJumping = true;
        _jumpingColliderSize = _currentColliderSize;
        //_currentColliderSize = new Vector2(1.26f, 1.06f);
        //_playerController.GetComponent<BoxCollider2D>().size = new Vector2(1.26f, 1.06f);
        Debug.Log("X collider size: " + _currentColliderSize.x + " Y collider size: " + _currentColliderSize.y);


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
                        && JumpsLeft >0
                        && !_playerController.Conditions.isFalling);
        // Double jump
        _animator.SetBool(_animatorDoubleJumpParameter, _playerController.Conditions.isJumping
                        && !_playerController.Conditions.isCollidingBelow
                        && JumpsLeft == 0
                        && !_playerController.Conditions.isFalling);
        // Fall
        /*_animator.SetBool(_animatorFallParameter, _playerController.Conditions.isFalling
                        && _playerController.Conditions.isJumping
                        && !_playerController.Conditions.isCollidingBelow);*/
    }
}
