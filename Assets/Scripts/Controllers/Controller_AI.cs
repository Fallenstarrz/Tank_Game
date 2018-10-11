using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI : MonoBehaviour
{
    /*[HideInInspector]*/ public TankData data;
    /*[HideInInspector]*/ public TankMotor motor;

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
        personality = (personalities)Random.Range(1, 4);

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
        motor.ShootMissile();
	}

    private void OnDestroy()
    {
        // Remove from GameManager List players
        GameManager.instance.aiUnits.Remove(this.data);
    }
}
