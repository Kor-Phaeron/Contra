using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerStates
{
    private int _shootAndRunForwardAnimatorParamter = Animator.StringToHash("Shoot and Run Forward");


    public override void SetAnimation()
    {
        _animator.SetBool(_shootAndRunForwardAnimatorParamter, _playerController.Conditions.isShooting
                            && Mathf.Abs(_horizontalInput) > 0.1f
                            );
    }
}
