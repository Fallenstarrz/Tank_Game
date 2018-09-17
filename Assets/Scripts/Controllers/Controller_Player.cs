using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    private TankData data;
    private TankMotor motor;

	void Start ()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
	}
	
	void Update ()
    {
        if (Input.GetButton("Shoot1"))
        {
            // Shoot machine gun
            motor.ShootBullet();
        }
        if (Input.GetButton("Shoot2"))
        {
            // Shoot cannon
            motor.ShootMissile();
        }
        if (Input.GetButton("MoveForward"))
        {
            // Move Forward
            Vector3 movementVector = (transform.forward * data.movementSpeed);
            motor.move(movementVector);
        }
        if (Input.GetButton("MoveBackward"))
        {
            // Move Backward
            Vector3 movementVector = (transform.forward * data.movementSpeed);
            motor.move(-movementVector);
        }
        if (Input.GetButton("RotateRight"))
        {
            // Move Right
            Vector3 vectorRotation = transform.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(vectorRotation);
        }
        if (Input.GetButton("RotateLeft"))
        {
            // Move Left
            Vector3 vectorRotation = transform.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(-vectorRotation);
        }
    }
}