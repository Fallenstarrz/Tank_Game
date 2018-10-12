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
    void Start ()
    {
		// Set mesh color to red
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
