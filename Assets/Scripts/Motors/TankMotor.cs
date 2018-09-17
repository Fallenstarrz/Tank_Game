using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    public CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void move(Vector3 movement)
    {
        controller.SimpleMove(movement);
    }

    public void rotate(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }

    public void ShootBullet()
    {

    }

    public void ShootMissile()
    {

    }
}
