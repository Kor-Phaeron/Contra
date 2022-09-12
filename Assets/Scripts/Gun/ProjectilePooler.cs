using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask collideWith;
    [SerializeField] private float projectileSkin = 0.05f;

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
            _gunProjectile.Speed * Time.deltaTime + projectileSkin, collideWith);

        if (hit)
        {
            _gunProjectile.DisableProjectile();
            gameObject.SetActive(false);
        }
    }
}
