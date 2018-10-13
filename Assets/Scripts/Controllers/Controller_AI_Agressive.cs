using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Agressive : MonoBehaviour
{
    // Agressive AI is supposed to track down and target keep running at them until either it dies or the player dies

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


        // FSM
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
        // run directly at the player position
        // continue to rotate towards player
        // if player is within X range, stop moving, but don't ever stop shooting at the player
        // if health is less than half
        // go into flee state
    }

    void stateFlee()
    {
        // run away from player for x seconds
        // then go back to patrol state
    }

    void statePatrol()
    {
        // walk around randomly
        // if player has been seen or heard
        // go into chase state
    }
}
