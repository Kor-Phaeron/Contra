using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerController.GetComponent<BoxCollider2D>().isTrigger = false;
            Debug.Log("Collided wth: " + collision.gameObject.tag);
        }
    }
}
