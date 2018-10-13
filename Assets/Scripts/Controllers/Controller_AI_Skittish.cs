using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Skittish : MonoBehaviour
{
    // Skittish AI is supposed to hit and run the player 1 shot at a time. 

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
        // go to last known player position
        // if we see player shoot at him and go back into flee state
        // if we don't see the player
        // go back into patrol state
    }

    void stateFlee()
    {
        // run away for X seconds
        // Go into chase state
    }

    void statePatrol()
    {
        // Walk around randomly
        // When player is seen, store their position as the last known position
        // Go into flee state
    }
}
