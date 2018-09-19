using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;

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
        // On start of bullet Destroy(gameObject, lifespan)
        // On start run function that applies rigidbody force to object
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    public void ShootMissile()
    {
        // On start of bullet Destroy(gameObject, lifespan)
        // On start run function that applies rigidbody force to object
        Instantiate(missilePrefab, firingPoint.position, firingPoint.rotation);
    }
}
