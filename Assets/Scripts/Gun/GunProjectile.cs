using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 30f;

    public Gun GunToShoot { get; set; }
    public Vector3 ShootDirection => _shootDirection;
    public float Speed { get; set; }
    private Vector3 _shootDirection;

    private void Awake()
    {
        Speed = speed;
    }

    void Update()
    {
        transform.Translate(_shootDirection * Speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 newDirection)
    {
        _shootDirection = newDirection;
    }

    public void EnableProjectile()
    {
        Speed = speed;
    }

    public void DisableProjectile()
    {
        Speed = 0f;
    }
}
