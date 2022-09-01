using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Image livesImage;
    [SerializeField] private GameObject[] playerLifes;

    private void OnPlayerLifes(int currentLifes)
    {
        for (int i = 0; i < playerLifes.Length; i++)
        {
            if (i < currentLifes)
            {
                playerLifes[i].gameObject.SetActive(true);
            }
            else
            {
                playerLifes[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        Health.OnLifesChange += OnPlayerLifes;
    }

    private void OnDisable()
    {
        Health.OnLifesChange -= OnPlayerLifes;
    }
}
