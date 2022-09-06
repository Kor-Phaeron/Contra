using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform levelStartPoint;

    private PlayerMotor _currentPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RevivePlayer();
        }
    }

    private void RevivePlayer()
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.gameObject.SetActive(true);
            _currentPlayer.SpawnPlayer(levelStartPoint);
            _currentPlayer.GetComponent<Health>().ResetLifes();
            _currentPlayer.gameObject.GetComponent<Animator>().SetBool("Double Jump", true);
        }
    }

    private void PlayerDeath(PlayerMotor player)
    {
        _currentPlayer = player;
        _currentPlayer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Health.OnDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        Health.OnDeath -= PlayerDeath;

    }
}