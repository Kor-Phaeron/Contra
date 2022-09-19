using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerStates
{
    private int _shootAndRunForwardAnimatorParamter = Animator.StringToHash("Shoot and Run Forward");
    private int _shootAndRunUpwardAnimatorParameter = Animator.StringToHash("Shoot and Run Upward");


    public override void SetAnimation()
    {
        _animator.SetBool(_shootAndRunForwardAnimatorParamter, _playerController.Conditions.isShootingAndRunningForward);
        _animator.SetBool(_shootAndRunUpwardAnimatorParameter, _playerController.Conditions.isShootingAndRunningUpward);
    }
}
