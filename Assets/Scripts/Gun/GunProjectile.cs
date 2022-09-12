using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 30f;

    public Gun GunToShoot { get; set; }

    private Vector3 _shootDirection;

    private void Start()
    {
        _shootDirection = GunToShoot.PlayerController.FacingRight ? Vector3.right : Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_shootDirection * speed * Time.deltaTime);
    }
}
