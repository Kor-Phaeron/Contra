using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GunProjectile projectilePrefab;
    [SerializeField] private Transform shotPoint;

    [Header("Gun Settings")]
    [SerializeField] private float msBetweenShots = 1000f;

    public PlayerController PlayerController { get; set; }
    private PlayerStates _playerStates;

    private ObjectPooler _pooler;
    private float _nextShotTime;
    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        PlayerController = GetComponent<PlayerController>();
        _playerStates = GetComponent<PlayerStates>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        else
        {
            PlayerController.Conditions.isShootingAndRunningForward = false;
            PlayerController.Conditions.isShootingAndRunningUpward = false;
        }
    }

    private void FireProjectle()
    {
        // Get Object from pool
        GameObject newProjectile = _pooler.GetObjectFromPool();
        newProjectile.transform.position = shotPoint.position;
        newProjectile.SetActive(true);

        // Get projectile
        GunProjectile projectile = newProjectile.GetComponent<GunProjectile>();
        projectile.GunToShoot = this;

        if (PlayerController.Conditions.isShootingAndRunningForward)
        {
            projectile.SetDirection(PlayerController.FacingRight ? Vector3.right : Vector3.left);
        }
        else if (PlayerController.Conditions.isShootingAndRunningUpward)
        {
            Debug.Log("Shooting upward");
            projectile.SetDirection(PlayerController.FacingRight ? new Vector3(0.5f, 0.5f, 0) : new Vector3(-0.5f, 0.5f, 0));
        }
        else
        {
            projectile.SetDirection(PlayerController.FacingRight ? Vector3.right : Vector3.left);
        }
        projectile.EnableProjectile();
    }

    public void Shoot()
    {
        if (Time.time > _nextShotTime)
        {
            _nextShotTime = Time.time + msBetweenShots / 1000f;
            if (_playerStates._horizontalInput == 0f && _playerStates._verticalInput == 0f)
            {
                PlayerController.Conditions.isShootingAndRunningForward = false;
                PlayerController.Conditions.isShootingAndRunningUpward = false;
            }
            else if (Mathf.Abs(_playerStates._horizontalInput) > 0.1f
                            && _playerStates._verticalInput == 0f)
            {
                PlayerController.Conditions.isShootingAndRunningUpward = false;
                PlayerController.Conditions.isShootingAndRunningForward = true;
            }
            else if (Mathf.Abs(_playerStates._horizontalInput) > 0.1f
                            && _playerStates._verticalInput > 0.1f)
            {
                PlayerController.Conditions.isShootingAndRunningForward = false;
                PlayerController.Conditions.isShootingAndRunningUpward = true;
            }
            FireProjectle();
            //PlayerController.Conditions.isShootingAndRunningForward = true;
        }
    }
}
