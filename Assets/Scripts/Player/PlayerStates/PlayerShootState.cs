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


    public override void SetAnimation()
    {
        _animator.SetBool(_shootAndRunForwardAnimatorParamter, _playerController.Conditions.isShootingAndRunningForward);
        _animator.SetBool(_shootAndRunUpwardAnimatorParameter, _playerController.Conditions.isShootingAndRunningUpward);
        _animator.SetBool(_shootAndRunDownwardAnimatorParameter, _playerController.Conditions.isShootingAndRunningDownward);
        _animator.SetBool(_shootUpAnimatorParameter, _playerController.Conditions.isShootingUp);
        _animator.SetBool(_shootLyingAnimatorParameter, _playerController.Conditions.isShootingLying);
    }
}
