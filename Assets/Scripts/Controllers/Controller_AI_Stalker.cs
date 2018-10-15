using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Stalker : MonoBehaviour
{
    Controller_AI controller;

    public float healthLastKnown;
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
        controller = GetComponent<Controller_AI>();
        // Set mesh color to blue
        currentState = states.patrol;
        healthLastKnown = controller.data.healthCurrent;
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
        // go to players backward direction * X units (distance to maintain), this will make us face behind the player
        Vector3 targetPosition = (-(controller.distanceToMaintain * GameManager.instance.players[0].transform.forward));
        // If we are not far enough from the player
        if (Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position) <= controller.distanceToMaintain)
        {
            controller.obstacleAvoidanceMove();
            controller.motor.rotateTowards(targetPosition);
        }
        // when we get far enough behind the player, shoot at the player
        else
        {
            controller.motor.rotateTowards(GameManager.instance.players[0].transform.position - transform.position);
            controller.motor.ShootMissile();
        }
        // if we get hit
        // go into flee state
        if (controller.data.healthCurrent < healthLastKnown)
        {
            healthLastKnown = controller.data.healthCurrent;
            currentState = states.flee;
        }
        // if can no longer see or hear player, resume patrolling
        if (!controller.canHearTarget() && !controller.canSeeTarget())
        {
            currentState = states.patrol;
        }

    }

    void stateFlee()
    {
        if (controller.timeInFlee <= controller.timeToFlee)
        {
            // run away from player
            Vector3 directionToFlee = -(GameManager.instance.players[0].transform.position - transform.position);
            if (controller.canMove())
            {
                controller.obstacleAvoidanceMove();
                controller.motor.rotateTowards(directionToFlee);
            }
            controller.obstacleAvoidanceMove();
            // increase the time we have been fleeing
            controller.timeInFlee += Time.deltaTime;
        }
        else
        {
            controller.timeInFlee = 0;
            // then go back to patrol state
            currentState = states.patrol;
        }
    }

    void statePatrol()
    {
        // walk around randomly between waypoints
        Vector3 targetPosition = new Vector3(GameManager.instance.waypoints[controller.currentWaypoint].position.x, transform.position.y, GameManager.instance.waypoints[controller.currentWaypoint].position.z);
        Vector3 dirToWaypoint = targetPosition - transform.position;
        if (controller.canMove())
        {
            controller.obstacleAvoidanceMove();
            controller.motor.rotateTowards(dirToWaypoint);
        }
        controller.obstacleAvoidanceMove();

        if (Vector3.Distance(transform.position, targetPosition) <= controller.closeEnough)
        {
            controller.getNextWaypoint();
        }
        if (controller.canSeeTarget() || controller.canHearTarget())
        {
            // go into chase state
            currentState = states.chase;
        }
    }
}
