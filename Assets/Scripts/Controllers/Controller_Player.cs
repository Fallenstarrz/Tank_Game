using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    [HideInInspector] private TankData data;
    [HideInInspector] private TankMotor motor;

    public enum controlType
    {
        wasd,
        arrows,
        controller
    }
    public controlType selectedController;

    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();

        GameManager.instance.players.Add(this.data);
    }

	void Update ()
    {
        switch (selectedController)
        {
            case controlType.wasd:
                wasdControls();
                break;
            case controlType.arrows:
                arrowControls();
                break;
            case controlType.controller:
                controllerControls();
                break;
        }
    }

    private void wasdControls()
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
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(movementVector);
        }
        if (Input.GetButton("MoveBackward"))
        {
            // Move Backward
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(-movementVector);
        }
        if (Input.GetButton("RotateRight"))
        {
            // Move Right
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(vectorRotation);
        }
        if (Input.GetButton("RotateLeft"))
        {
            // Move Left
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(-vectorRotation);
        }
    }

    private void arrowControls()
    {
        if (Input.GetButton("Shoot1P2"))
        {
            // Shoot machine gun
            motor.ShootBullet();
        }
        if (Input.GetButton("Shoot2P2"))
        {
            // Shoot cannon
            motor.ShootMissile();
        }
        if (Input.GetButton("MoveForwardP2"))
        {
            // Move Forward
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(movementVector);
        }
        if (Input.GetButton("MoveBackwardP2"))
        {
            // Move Backward
            Vector3 movementVector = (Vector3.forward * data.movementSpeed * Time.deltaTime);
            motor.move(-movementVector);
        }
        if (Input.GetButton("RotateRightP2"))
        {
            // Move Right
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(vectorRotation);
        }
        if (Input.GetButton("RotateLeftP2"))
        {
            // Move Left
            Vector3 vectorRotation = Vector3.up * data.rotationSpeed * Time.deltaTime;
            motor.rotate(-vectorRotation);
        }
    }

    private void controllerControls()
    {
        if (Input.GetButton("CShoot1"))
        {
            // Shoot machine gun
            motor.ShootBullet();
        }
        if (Input.GetButton("CShoot2"))
        {
            // Shoot cannon
            motor.ShootMissile();
        }
        // Move Forward
        Vector3 movementVector = (-Vector3.forward * data.movementSpeed * Time.deltaTime * Input.GetAxis("CMoveForward"));
        motor.move(movementVector);
        // Move Right
        Vector3 vectorRotation = (Vector3.up * data.rotationSpeed * Time.deltaTime * Input.GetAxis("CRotateRight"));
        motor.rotate(vectorRotation);
    }

    private void OnDestroy()
    {
        // Play death sound
        if (data.death != null)
        {
            AudioSource.PlayClipAtPoint(data.death, transform.position);
        }
        // Add to GameManager List players
        GameManager.instance.players.Remove(this.data);
    }
}