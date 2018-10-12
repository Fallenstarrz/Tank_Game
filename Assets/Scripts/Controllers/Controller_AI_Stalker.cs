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
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
