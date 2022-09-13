using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlayerMotor _playerToFollow;
    [SerializeField] private bool horizontalFollow;
    [SerializeField] private bool verticalFollow;

    [Header("Horizontal")]
    [SerializeField][Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float horizontalSmoothness = 3f;

    [Header("Vertical")]
    [SerializeField][Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            CenterOnTarget(_playerToFollow);
        }
    }

    private Vector3 GetTargetPosition(PlayerMotor player)
    {
        float xPos = 0f;
        float yPos = 0f;

        xPos += (player.transform.position.x + horizontalOffset) * horizontalInfluence;
        yPos += (player.transform.position.y + verticalOffset) * verticalInfluence;

        Vector3 positionTarget = new Vector3(xPos, yPos, transform.position.z);

        return positionTarget;
    }

    private void CenterOnTarget(PlayerMotor player)
    {
        Vector3 targetPosition = GetTargetPosition(player);
        transform.localPosition = targetPosition;
    }
}
