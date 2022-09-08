using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected PlayerMotor _playerMotor;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }


    private void CollectLogic()
    {
        if (!CanBePicked())
        {
            return;
        }

        Collect();
        DisableCollectable();
    }

    protected virtual void Collect()
    {
        Debug.Log("Collected");
    }

    private void DisableCollectable()
    {
        _collider2D.enabled = false;
        _spriteRenderer.enabled = false;
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
