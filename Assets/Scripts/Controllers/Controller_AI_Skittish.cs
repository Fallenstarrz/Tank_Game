using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI_Skittish : MonoBehaviour
{
    Controller_AI controller;

    Vector3 lastKnownPosition;

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
        if (controller.targetPlayer != null)
        {
            // go to last known player position if we don't see target
            foreach (TankData targetable in GameManager.instance.players)
            {
                if (controller.canSeeTarget(targetable) || controller.canHearTarget(targetable))
                {
                    if (Vector3.Distance(transform.position, lastKnownPosition) >= controller.closeEnough)
                    {
                        controller.motor.rotateTowards(lastKnownPosition - transform.position);
                        controller.obstacleAvoidanceMove();
                    }
                    else
                    {
                        currentState = states.patrol;
                    }
                }
                // if we see player shoot at him and go back into flee state
                else
                {
                    controller.motor.rotateTowards(controller.targetPlayer.transform.position - transform.position);
                    if (Vector3.Angle(transform.position, controller.targetPlayer.transform.position) < controller.skittishShootingAngle)
                    {
                        controller.motor.ShootMissile();
                        lastKnownPosition = controller.targetPlayer.transform.position;
                        currentState = states.flee;
                    }
                }
            }
        }
        else
        {
            currentState = states.patrol;
        }
    }

    void stateFlee()
    {
        if (controller.targetPlayer != null)
        {
            if (controller.timeInFlee <= controller.timeToFlee)
            {
                // run away from player
                Vector3 directionToFlee = -(controller.targetPlayer.transform.position - transform.position);
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
                currentState = states.chase;
            }
        }
        else
        {
            currentState = states.patrol;
        }
    }

    void statePatrol()
    {
        // he is a scared AI, so he just kind stays in 1 spot and rotates. We didn't want all the AIs to patrol the same way.
        controller.motor.rotate(Vector3.up * controller.data.rotationSpeed * Time.deltaTime);
        // when we see or hear a noise remember the location of the noise and run away!
        foreach (TankData targetable in GameManager.instance.players)
        {
            if (controller.canSeeTarget(targetable) || controller.canHearTarget(targetable))
            {
                controller.targetPlayer = controller.aquireTarget();
                // remember location of the noise
                lastKnownPosition = controller.targetPlayer.transform.position;
                // go into flee state
                currentState = states.flee;
            }
        }
    }
}
