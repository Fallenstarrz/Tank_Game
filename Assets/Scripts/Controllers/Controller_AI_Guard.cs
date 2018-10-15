﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Guard : MonoBehaviour
{
    Controller_AI controller;

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
        controller = GetComponent<Controller_AI>();
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
        // Stand ground and shoot at the target
        Vector3 targetLocation = GameManager.instance.players[0].transform.position - transform.position;
        // rotate to players position
        controller.motor.rotateTowards(targetLocation);
        // keep firing on cooldown
        controller.motor.ShootMissile();

        // if health is below half
        // go into flee state
        if (controller.data.healthCurrent <= (controller.data.healthMax / 2)) // TODO: Probably change to a percentage threshhold, but we will leave at 1/2 for now
        {
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
        float distanceFromTarget = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
        if (controller.distanceToMaintain >= distanceFromTarget)
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
        }
        else
        {
            // then go back to chase state
            currentState = states.chase;
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
        if (controller.canHearTarget() || controller.canSeeTarget())
        {
            // go into chase state
            currentState = states.chase;
        }
    }
}
