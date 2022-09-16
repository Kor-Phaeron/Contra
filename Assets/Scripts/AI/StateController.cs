using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    private void Update()
    {
        currentState.RunState(this);
    }

    public void TransitionToState(AIState newState)
    {
        if (newState != remainState)
        {
            currentState = newState;
        }
    }
}
