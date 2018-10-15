﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Agressive : MonoBehaviour
{
    Controller_AI controller;

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
        controller = GetComponent<Controller_AI>();
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
        // if player is within X range, stop moving, but don't ever stop shooting at the player
        Vector3 targetLocation = GameManager.instance.players[0].transform.position - transform.position;

        if (Vector3.Distance(GameManager.instance.players[0].transform.position, transform.position) >= controller.distanceToMaintain)
        {
            if (controller.canMove())
            {
                controller.obstacleAvoidanceMove();
                controller.motor.rotateTowards(targetLocation);
            }
            controller.obstacleAvoidanceMove();
            controller.motor.ShootMissile();
        }
        else
        {
            controller.motor.rotateTowards(targetLocation);
            controller.motor.ShootMissile();
        }
        if (controller.data.healthCurrent <= (controller.data.healthMax/2)) // TODO: Probably change to a percentage threshhold, but we will leave at 1/2 for now
        {
            currentState = states.flee;
        }
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
