using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Guard : MonoBehaviour
{
    // Guard AI is supposed to stay in 1 area and patrol around "protecting" the zone

    public enum states
    {
        chase,
        flee,
        patrol
    };
    public states currentState;

    // Use this for initialization
    void Start()
    {
        // Set mesh color to blue
        currentState = states.patrol;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case states.chase:
                stateChase();
                break;
            case states.flee:
                stateFlee();
                break;
            case states.patrol:
                statePatrol();
                break;
        }
    }

    void stateChase()
    {
        // stand your ground
        // rotate to players position
        // keep firing on cooldown
        // if health is below half
        // go into flee state
    }

    void stateFlee()
    {
        // stay in flee state for X seconds
        // return to patrolling state
    }

    void statePatrol()
    {
        // Walk around your waypoints in an organized fashion
        // if we see player
        // go into chase state
    }
}
