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
    void Start ()
    {
		// Set mesh color to green
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
