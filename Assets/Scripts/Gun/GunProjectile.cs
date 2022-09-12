using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 30f;

    public Gun GunToShoot { get; set; }
    public Vector3 ShootDirection => _shootDirection;
    public float Speed => speed;
    private Vector3 _shootDirection;

    void Update()
    {
        transform.Translate(_shootDirection * speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 newDirection)
    {
        _shootDirection = newDirection;
    }
}
