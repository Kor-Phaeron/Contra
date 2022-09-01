using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    // Create an event
    public static Action<int> OnLifesChange;
    public static Action<PlayerMotor> OnDeath;

    [Header("Settings")]
    [SerializeField] private int lifes = 2;

    private int _maxLifes;
    private int _currentLifes;

    private void Awake()
    {
        _maxLifes = _currentLifes;
    }

    private void Start()
    {
        ResetLifes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            LoseLife();
        }
    }

    private void AddLife()
    {
        _currentLifes += 1;
        if (_currentLifes > _maxLifes)
        {
            _currentLifes = _maxLifes;
        }
        UpdateLifesUI();
    }

    private void LoseLife()
    {
        _currentLifes -= 1;
        if (_currentLifes <= 0)
        {
            _currentLifes = 0;
            OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
        }
        UpdateLifesUI();
    }

    public void ResetLifes()
    {
        _currentLifes = lifes;
        UpdateLifesUI();
    }

    private void UpdateLifesUI()
    {
        OnLifesChange?.Invoke(_currentLifes);
    }
}
