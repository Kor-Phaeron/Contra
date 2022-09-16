using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action<PlayerMotor> OnPlayerSpawn;

    [Header("Settings")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject playerPrefab;

    private PlayerMotor _currentPlayer;

    private void Awake()
    {
        SpawnPlayer(playerPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RevivePlayer();
        }
    }

    private void SpawnPlayer(GameObject player)
    {
        _currentPlayer = Instantiate(player, levelStartPoint.position, Quaternion.identity).GetComponent<PlayerMotor>();

        // Call event
        OnPlayerSpawn?.Invoke(_currentPlayer);
    }


    private void RevivePlayer()
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.gameObject.SetActive(true);
            _currentPlayer.SpawnPlayer(levelStartPoint);
            _currentPlayer.GetComponent<Health>().ResetLifes();
            _currentPlayer.gameObject.GetComponent<Animator>().SetBool("Double Jump", true);
            _currentPlayer.GetComponent<Health>().Revive();
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
