using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{

    private void Start()
    {

    }

    public void move(Vector3 movement)
    {
        transform.Translate(movement);
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
