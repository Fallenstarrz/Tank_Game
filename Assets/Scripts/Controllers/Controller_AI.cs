using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_AI : MonoBehaviour
{
    [HideInInspector] public TankData data;
    [HideInInspector] public TankMotor motor;

	// Use this for initialization
	void Start ()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        GameManager.instance.aiUnits.Add(this.data);
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
