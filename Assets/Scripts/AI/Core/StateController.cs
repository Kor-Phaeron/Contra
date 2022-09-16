using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentState.RunState(this);
        if (Input.GetKeyDown(KeyCode.T))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            MoveRight();
        }
    }

    public void TransitionToState(AIState newState)
    {
        if (newState != remainState)
        {
            currentState = newState;
        }
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-1f * 4f, rb.velocity.y);
        //Debug.Log("Left");
        //rb.transform.localScale = new Vector3(1f, 1f, 1f);
        //rb.transform.Translate(Vector3.left * 4f * Time.deltaTime);
    }
    private void MoveRight()
    {
        rb.velocity = new Vector2(1f * 4f, rb.velocity.y);
        //Debug.Log("Left");
        //rb.transform.localScale = new Vector3(-1f, 1f, 1f);
        //rb.transform.Translate(Vector3.right * 4f * Time.deltaTime);
    }
}
