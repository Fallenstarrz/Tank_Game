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
    void Start ()
    {
		// Set mesh color to yellow
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
