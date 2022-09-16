using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float gravity = -20.0f;
    [SerializeField] private float skin = 0.05f;
    [SerializeField] private float fallMultiplier = 1.5f;

    [Header("Collisions")]
    [SerializeField] private LayerMask collideWith;
    [SerializeField] private int verticalRayAmount = 4;
    [SerializeField] private int horizontalRayAmaount = 4;

    #region Properties
    public bool FacingRight { get; set; }
    public float Gravity => gravity;
    public PlayerConditions Conditions => _playerConditions;
    public Vector2 Force => _force;
    #endregion


    #region Internal

    private PlayerConditions _playerConditions;
    private BoxCollider2D _boxCollider2D;
    private Vector2 _boundsBottomRight;
    private Vector2 _boundsTopRight;
    private Vector2 _boundsBottomLeft;
    private Vector2 _boundsTopLeft;

    private float _boundsHeight;
    private float _boundsWidth;

    private float _currentGravity;

    public Animator _animator;

    private Vector2 _force;
    private Vector2 _movePosition; // Next position where we want our character to be

    private float _internalFaceDirection = 1f;
    private float _faceDirection;

    #endregion

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerConditions = new PlayerConditions();
        _playerConditions.ResetConditions();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        ApplyGravity();
        StartMoving();
        SetRayOrigins();
        GetFaceDirection();
        RotateModel();
        if (FacingRight)
        {
            CollisionHorizontal(1);
        }
        else
        {
            CollisionHorizontal(-1);
        }
        CollisionBelow();
        CollisionAbove();
        

        transform.Translate(_movePosition, Space.Self);

        /*        Debug.DrawRay(_boundsBottomRight, Vector2.right, Color.red);
                Debug.DrawRay(_boundsTopRight, Vector2.right, Color.red);
                Debug.DrawRay(_boundsBottomLeft, Vector2.left, Color.red);
                Debug.DrawRay(_boundsTopLeft, Vector2.left, Color.red);*/

        SetRayOrigins();
        CalculateMovement();
    }

    #region Collisions

    #region Collision below
    private void CollisionBelow()
    {
        if (_movePosition.y < -0.0001f) // If move position is less than -0.0001f by Y axis then we're falling
        {
            _playerConditions.isFalling = true;
        }
        else
        {
            _playerConditions.isFalling = false;
        }

        if (!_playerConditions.isFalling) // If we're not falling, then going up. No need for further calculations of colliding below
        {
            _playerConditions.isCollidingBelow = false;
            return;
        }

        // Calculate ray length
        float rayLength = _boundsHeight / 2f + skin;
        if (_movePosition.y < 0)
        {
            rayLength += Mathf.Abs(_movePosition.y);
        }

        // Calculate ray origin
        Vector2 leftOrigin = (_boundsBottomLeft + _boundsTopLeft) / 2f;
        Vector2 rightOrigin = (_boundsBottomRight + _boundsTopRight) / 2f;

        leftOrigin += (Vector2)(transform.up * skin) + (Vector2)(transform.right * _movePosition.x);
        rightOrigin += (Vector2)(transform.up * skin) + (Vector2)(transform.right * _movePosition.x);

        // Raycast
        for (int i = 0; i < verticalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(leftOrigin, rightOrigin, (float) i / (float) (verticalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -transform.up, rayLength, collideWith);
            Debug.DrawRay(rayOrigin, -transform.up * rayLength, Color.red);

            if (hit)
            {
                if (_force.y > 0f)
                {
                    _movePosition.y = _force.y * Time.deltaTime;
                    _playerConditions.isCollidingBelow = false;
                }

                else 
                {
                    _movePosition.y = -hit.distance + _boundsHeight / 2f + skin;
                }

                _playerConditions.isCollidingBelow = true;
                _playerConditions.isFalling = false;
                _animator.SetBool("Jump", false);



                if (Mathf.Abs(_movePosition.y) < 0.0001f)
                {
                    _movePosition.y = 0f;
                }
            }
        }
    }
    #endregion

    #region Collision Above

    private void CollisionAbove()
    {
        if (_movePosition.y < 0)
        {
            return;
        }
        // Set ray lenght
        float rayLenght = _movePosition.y + _boundsHeight / 2f;

        // Origin points
        Vector2 rayTopLeft = (_boundsBottomLeft + _boundsTopLeft) / 2f;
        Vector2 rayTopRight = (_boundsBottomRight + _boundsTopRight) / 2f;
        rayTopLeft += (Vector2) transform.right * _movePosition.x;
        rayTopRight += (Vector2)transform.right * _movePosition.x;

        for (int i = 0; i < verticalRayAmount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(rayTopLeft, rayTopRight, (float)i / (float)(verticalRayAmount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, transform.up, rayLenght, collideWith);
            Debug.DrawRay(rayOrigin, transform.up * rayLenght, Color.red);

            if (hit)
            {
                _movePosition.y = hit.distance - _boundsHeight / 2f;
                _playerConditions.isCollidingAbove = true;
            }
            else
            {
                _playerConditions.isCollidingAbove = false;
            }
        }
    }

    #endregion

    #region Collision horizontal

    private void CollisionHorizontal(int direction)
    {
        float rayLength = Mathf.Abs(_force.x * Time.deltaTime) + _boundsWidth / 2 + skin * 2;

        Vector2 topOrigin = (_boundsTopLeft + _boundsTopRight) / 2f;
        Vector2 bottomOrigin = (_boundsBottomLeft + _boundsBottomRight) / 2f;

        topOrigin -= (Vector2)transform.up * skin;
        bottomOrigin += (Vector2)transform.up * skin;

        for (int i = 0; i < horizontalRayAmaount; i++)
        {
            Vector2 rayOrigin = Vector2.Lerp(topOrigin, bottomOrigin, (float) i / (float) (horizontalRayAmaount - 1));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction * transform.right, rayLength, collideWith);
            Debug.DrawRay(rayOrigin, transform.right * rayLength * direction, Color.red);

            if (hit)
            {
                if (direction >= 0)
                {
                    _movePosition.x = hit.distance - _boundsWidth / 2f - skin / 2f;
                }
                else
                {
                    _movePosition.x = -hit.distance + _boundsWidth / 2f + skin / 2f;
                }

                _force.x = 0f;
            }
        }
    }

    #endregion

    #endregion

    #region Movement

    private void CalculateMovement()
    {
        if (Time.deltaTime > 0)
        {
            _force = _movePosition / Time.deltaTime; // If _movePosition is equal to 0 than no _force being add in update method
        }
    }

    private void StartMoving()
    {
        _movePosition = _force * Time.deltaTime; // Moving our character using Vector2 force
        _playerConditions.ResetConditions();
    }

    public void SetHorizontalForce(float xForce) // Actual moving in horizontal axis
    {
        _force.x = xForce;
    }

    public void SetVerticalForce(float yForce)
    {
        _force.y = yForce;
    }


    private void ApplyGravity()
    {
        _currentGravity = gravity;
        if (_force.y < 0)
        {
            _currentGravity *= fallMultiplier;
        }
        _force.y += _currentGravity * Time.deltaTime; // Applying force in Y axis to imitate gravity. Gravity set in the settings
    }

    #endregion

    #region Direction

    private void GetFaceDirection()
    {
        _faceDirection = _internalFaceDirection;
        FacingRight = _faceDirection == 1;
        if (_force.x > 0.0001f)
        {
            _faceDirection = 1f;
            FacingRight = true;
        }
        else if (_force.x < -0.0001f)
        {
            _faceDirection = -1f;
            FacingRight = false;
        }

        _internalFaceDirection = _faceDirection;
    }

    private void RotateModel()
    {
        if (FacingRight)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    #endregion

    #region Ray origins
    private void SetRayOrigins()
    {
        Bounds playerBounds = _boxCollider2D.bounds;
        _boundsBottomRight = new Vector2(playerBounds.max.x, playerBounds.min.y);
        _boundsTopRight = new Vector2(playerBounds.max.x, playerBounds.max.y);
        _boundsBottomLeft = new Vector2(playerBounds.min.x, playerBounds.min.y);
        _boundsTopLeft = new Vector2(playerBounds.min.x, playerBounds.max.y);

        _boundsHeight = Vector2.Distance(_boundsBottomRight, _boundsTopRight);
        _boundsWidth = Vector2.Distance(_boundsBottomLeft, _boundsBottomRight);
    }
    #endregion

}
