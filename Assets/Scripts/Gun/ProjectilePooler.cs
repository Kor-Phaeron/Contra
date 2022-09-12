using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask collideWith;

    private GunProjectile _gunProjectile;

    private void Start()
    {
        _gunProjectile = GetComponent<GunProjectile>();
    }

    private void Update()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _gunProjectile.ShootDirection,
            _gunProjectile.Speed * Time.deltaTime + 0.1f, collideWith);

        if (hit)
        {
            gameObject.SetActive(false);
        }
    }
}
