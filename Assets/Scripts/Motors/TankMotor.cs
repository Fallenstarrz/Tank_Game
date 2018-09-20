using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    [HideInInspector] public TankData data;

    private void Start()
    {
        data = GetComponent<TankData>();
    }

    private void Update()
    {
        if (data.missileCooldownCurrent >= 0)
        {
            data.missileCooldownCurrent -= Time.deltaTime;
        }
        if (data.bulletCooldownCurrent >= 0)
        {
            data.bulletCooldownCurrent -= Time.deltaTime;
        }
    }

    public void move(Vector3 movement)
    {
        transform.Translate(movement);
    }

    public void rotate(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }

    bool checkCooldown(float cooldown)
    {
        if (cooldown >= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ShootBullet()
    {
        if (checkCooldown(data.bulletCooldownCurrent))
        {
            data.bulletCooldownCurrent = data.bulletCooldownMax;
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        }
    }

    public void ShootMissile()
    {
        if (checkCooldown(data.missileCooldownCurrent))
        {
            data.missileCooldownCurrent = data.missileCooldownMax;
            Instantiate(missilePrefab, firingPoint.position, firingPoint.rotation);
        }
    }
}
