using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI : MonoBehaviour
{
    [HideInInspector] public TankData data;
    [HideInInspector] public TankMotor motor;
    public List<Transform> waypoints;
    public int currentWaypoint;

    public enum personalities
    {
        stalker,
        skittish,
        guard,
        agressive
    };
    public personalities personality;

	// Use this for initialization
	void Start ()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        GameManager.instance.aiUnits.Add(this.data);
        personality = (personalities)Random.Range(0, 4);

        switch (personality)
        {
            case personalities.agressive:
                gameObject.AddComponent<Controller_AI_Agressive>();
                break;
            case personalities.skittish:
                gameObject.AddComponent<Controller_AI_Skittish>();
                break;
            case personalities.stalker:
                gameObject.AddComponent<Controller_AI_Stalker>();
                break;
            case personalities.guard:
                gameObject.AddComponent<Controller_AI_Guard>();
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Debug line to check if floor exists
        // Debug.DrawRay(transform.position + (transform.forward * data.wallDetectDistance), -transform.up, Color.red);
        // Debug line to check if forward direction is obstructed
        // Debug.DrawRay(transform.position, transform.forward * data.wallDetectDistance, Color.blue);

        // If there are no obstacles obstructing our movement path, move forward
        if (canMove())
        {
            motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
        }
        else
        {
            // If there is a floor to walk on, move forward
            if (floorExists())
            {
                motor.move(Vector3.forward * data.movementSpeed * Time.deltaTime);
            }
            // If there is no floor to walk on, rotate until we find a floor to walk on
            else
            {
                motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
            }
        }
        canSeeTarget();
    }

    private void OnDestroy()
    {
        // Remove from GameManager List players
        GameManager.instance.aiUnits.Remove(this.data);
    }

    bool canMove()
    {
        // If raycast hits nothing return true
        if (Physics.Raycast(transform.position, transform.forward, data.wallDetectDistance))
        {
            return true;
        }
        // else return false
        return false;
    }

    bool floorExists()
    {
        // If raycast hits nothing return false, floor doesn't exist
        if (Physics.Raycast(transform.position + (transform.forward * data.wallDetectDistance), -transform.up, data.wallDetectDistance))
        {
            return true;
        }
        // else return true
        return false;
    }

    bool canSeeTarget()
    {
        // Create a vector to target by getting the target position and subtracting our current position
        Vector3 vectorToTarget = (GameManager.instance.players[0].transform.position - transform.position);
        // Create an angle to target by Vector3.Angle (vectorToTarget, transform.forward)
        float Angle = Vector3.Angle(vectorToTarget, transform.forward);

        // if target is not within field of view, return false
        if (Angle > data.fieldOfView)
        {
            return false;
        }

        // if raycast target hitInfo is null, return false
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, vectorToTarget, out hitInfo, data.viewDistance);
        if (hitInfo.collider == null)
        {
            return false;
        }

        // if target hit with raycast != target player, return false
        Collider targetCollider = GameManager.instance.players[0].GetComponent<Collider>();
        if (targetCollider != hitInfo.collider)
        {
            return false;
        }

        // otherwise we can see the player!, return true
        return true;
    }

    bool canHearTarget()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);
        return distance < data.hearingDistance;
    }
}