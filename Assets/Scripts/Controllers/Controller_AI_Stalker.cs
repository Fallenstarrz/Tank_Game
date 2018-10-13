using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Stalker : MonoBehaviour
{
    // Stalker AI is supposed to try to stay behind the player and shoot at them when within range

    public enum states
    {
        chase,
        flee,
        patrol
    };
    public states currentState;

	// Use this for initialization
	void Start ()
    {
        // Set mesh color to blue
        currentState = states.patrol;
	}
	
	// Update is called once per frame
	void Update ()
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
        // go to players position + players transform.forward - X units (distance to maintain)
        // when we arrive there, shoot at the player
        // if the player is facing us
        // go into flee state
    }

    void stateFlee()
    {
        // for X seconds stay in flee state
        // run away from player
        // go back to patrol state
    }

    void statePatrol()
    {
        // walk around randomly
        // if we see or hear player
        // go into chase state
    }
}
