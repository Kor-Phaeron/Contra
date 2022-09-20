using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerStates
{
    private int _shootAndRunForwardAnimatorParamter = Animator.StringToHash("Shoot and Run Forward");
    private int _shootAndRunUpwardAnimatorParameter = Animator.StringToHash("Shoot and Run Upward");
    private int _shootAndRunDownwardAnimatorParameter = Animator.StringToHash("Shoot and Run Downward");
    private int _shootUpAnimatorParameter = Animator.StringToHash("Shoot Up");
    private int _shootLyingAnimatorParameter = Animator.StringToHash("Shoot Lying");

    private void Update()
    {
        CheckShootingState();
    }

    private void CheckShootingState()
    {
        _playerController.Conditions.isShootingAndRunningForward = false;
        _playerController.Conditions.isShootingAndRunningUpward = false;
        _playerController.Conditions.isShootingAndRunningDownward = false;
        _playerController.Conditions.isShootingUp = false;
        _playerController.Conditions.isShootingLying = false;

        if (Mathf.Abs(_horizontalInput) > 0.1f
               && _verticalInput == 0f
               && _playerController.Conditions.isCollidingBelow
               && !_playerController.Conditions.isJumping)
        {
            _playerController.Conditions.isShootingAndRunningForward = true;
        }

        else if (Mathf.Abs(_horizontalInput) > 0.1f
                        && _verticalInput > 0.1f
                        && _playerController.Conditions.isCollidingBelow
                        && !_playerController.Conditions.isJumping)
        {
            _playerController.Conditions.isShootingAndRunningUpward = true;
        }

        else if (Mathf.Abs(_horizontalInput) > 0.1f
                        && _verticalInput < -0.1f
                        && _playerController.Conditions.isCollidingBelow
                        && !_playerController.Conditions.isJumping)
        {
            _playerController.Conditions.isShootingAndRunningDownward = true;
        }

        else if (_horizontalInput == 0f
                        && _verticalInput > 0.1f
                        && _playerController.Conditions.isCollidingBelow
                        && !_playerController.Conditions.isJumping)
        {
            _playerController.Conditions.isShootingUp = true;
        }

        else if (_horizontalInput == 0f
                        && _verticalInput < -0.1f
                        && _playerController.Conditions.isCollidingBelow
                        && !_playerController.Conditions.isJumping)
        {
            _playerController.Conditions.isShootingLying = true;
        }
    }

    public override void SetAnimation()
    {
        _animator.SetBool(_shootAndRunForwardAnimatorParamter, _playerController.Conditions.isShootingAndRunningForward);
        _animator.SetBool(_shootAndRunUpwardAnimatorParameter, _playerController.Conditions.isShootingAndRunningUpward);
        _animator.SetBool(_shootAndRunDownwardAnimatorParameter, _playerController.Conditions.isShootingAndRunningDownward);
        _animator.SetBool(_shootUpAnimatorParameter, _playerController.Conditions.isShootingUp);
        _animator.SetBool(_shootLyingAnimatorParameter, _playerController.Conditions.isShootingLying);
    }
}
