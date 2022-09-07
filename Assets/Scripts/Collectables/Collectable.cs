using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private PlayerMotor _playerMotor;

    private void CollectLogic()
    {
        if (!CanBePicked())
        {
            return;
        }

        Collect();
    }

    protected virtual void Collect()
    {
        Debug.Log("Collected");
    }

    private bool CanBePicked()
    {
        return _playerMotor != null; // the same as below

        /*if (_playerMotor == null)
        {
            return false;
        }

        return true;*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMotor>() != null)
        {
            _playerMotor = collision.GetComponent<PlayerMotor>();
            CollectLogic();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerMotor = null;
    }
}
