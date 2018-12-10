using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI : MonoBehaviour
{
    // TODO: Add a way for AI to select a player as the target, or loop through them to check all the targets
    [HideInInspector] public TankData data;
    [HideInInspector] public TankMotor motor;
    public List<Transform> waypoints;
    public int currentWaypoint = 0;

    public TankData targetPlayer;

    public float timeInFlee;
    public float timeToFlee;

    public float closeEnough;
    public float distanceToMaintain;
    public float distanceToMaintainGuard;
    public float distanceToMaintainAgressive;
    public float distanceToMaintainStalker;
    public float distanceToMaintainSkittish;

    public float skittishShootingAngle;

    public MeshRenderer topColor;
    public MeshRenderer leftColor;
    public MeshRenderer rightColor;
    public MeshRenderer bottomColor;

    [HideInInspector]public enum personalities
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

       // Can use this script to randomly and dynamically select a personality for your AIs

       personality = (personalities)Random.Range(0, System.Enum.GetNames(typeof(personalities)).Length);
       // Debug purposes only: Uncomment the AI type you want to test and comment out the rest including the random above. Default should be random
       // When finished testing make sure you uncomment the 
       //personality = personalities.agressive;
       //personality = personalities.guard;
       //personality = personalities.skittish;
       //personality = personalities.stalker;
       switch (personality)
       {
            case personalities.agressive:
                gameObject.AddComponent<Controller_AI_Agressive>();
                distanceToMaintain = distanceToMaintainAgressive;
                topColor.materials[0].color = Color.red;
                leftColor.materials[0].color = Color.red;
                rightColor.materials[0].color = Color.red;
                bottomColor.materials[0].color = Color.red;
                break;
            case personalities.skittish:
                gameObject.AddComponent<Controller_AI_Skittish>();
                distanceToMaintain = distanceToMaintainGuard;
                topColor.materials[0].color = Color.white;
                leftColor.materials[0].color = Color.white;
                rightColor.materials[0].color = Color.white;
                bottomColor.materials[0].color = Color.white;
                break;
            case personalities.stalker:
                gameObject.AddComponent<Controller_AI_Stalker>();
                distanceToMaintain = distanceToMaintainStalker;
                topColor.materials[0].color = Color.yellow;
                leftColor.materials[0].color = Color.yellow;
                rightColor.materials[0].color = Color.yellow;
                bottomColor.materials[0].color = Color.yellow;
                break;
            case personalities.guard:
                gameObject.AddComponent<Controller_AI_Guard>();
                distanceToMaintain = distanceToMaintainGuard;
                topColor.materials[0].color = Color.black;
                leftColor.materials[0].color = Color.black;
                rightColor.materials[0].color = Color.black;
                bottomColor.materials[0].color = Color.black;
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
    }

    private void OnDestroy()
    {
        // Play death sound
        if (data.death != null)
        {
            AudioSource.PlayClipAtPoint(data.death, transform.position, (GameManager.instance.sfxVol + 80) / 80);
        }
        // Remove from GameManager List players
        GameManager.instance.aiUnits.Remove(this.data);
        // TODO: TEMPORARY FIX
        if (GameManager.instance.numAICurrent != 0)
        {
            GameManager.instance.numAICurrent--;
        }
    }

    public bool canMove()
    {
        // If raycast hits nothing return true
        if (Physics.Raycast(transform.position, transform.forward, data.wallDetectDistance))
        {
            return false;
        }
        // else return false
        return true;
    }

    public bool floorExists()
    {
        // If raycast hits nothing return false, floor doesn't exist
        if (Physics.Raycast(transform.position + (transform.forward * data.wallDetectDistance), -transform.up, data.wallDetectDistance))
        {
            return true;
        }
        // else return true
        return false;
    }

    public bool canSeeTarget(TankData target)
    {
        // Create a vector to target by getting the target position and subtracting our current position
        Vector3 vectorToTarget = (target.transform.position - transform.position);
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
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider != hitInfo.collider)
        {
            return false;
        }

        // otherwise we can see the player!, return true
        return true;
    }

    // Return true/false if target made a noise within hearing distance
    public bool canHearTarget(TankData target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance >= (target.noiseLevel + data.hearingDistance))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public TankData aquireTarget()
    {
        if (canHearTarget(GameManager.instance.players[0]) || canSeeTarget(GameManager.instance.players[0]))
        {
            return GameManager.instance.players[0];
        }
        if (canHearTarget(GameManager.instance.players[1]) || canSeeTarget(GameManager.instance.players[1]))
        {
            return GameManager.instance.players[1];
        }
        return null;
    }

    public void obstacleAvoidanceMove()
    {
        // If there are no obstacles obstructing our movement path, move forward
        if (canMove())
        {
            // If there is a floor to walk on, move forward
            if (floorExists())
            {
                motor.move(Vector3.forward * data.movementSpeed * Time.deltaTime);
            }
            else
            {
                motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            motor.rotate(Vector3.up * data.rotationSpeed * Time.deltaTime);
        }
    }
    // Sets next waypoint
    public void getNextWaypoint()
    {
        int maxWaypoints = waypoints.Count - 1;
        if (currentWaypoint < maxWaypoints)
        {
            currentWaypoint++;
        }
        else
        {
            currentWaypoint = 0;
        }
    }
}