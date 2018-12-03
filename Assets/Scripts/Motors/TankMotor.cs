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

    // Reduce current cooldowns, so the skill can eventually be used again
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
        if (data.noiseLevel >= 0)
        {
            data.noiseLevel -= data.noiseLevelReducPerSec;
        }
    }

    // move the character by x (parameter)
    public void move(Vector3 movement)
    {
        transform.Translate(movement);
        data.noiseLevel = data.moveNoiseLevel;
    }

    // rotate the character by x (parameter)
    public void rotate(Vector3 rotation)
    {
        transform.Rotate(rotation);
        data.noiseLevel = data.rotateNoiseLevel;
    }

    // check the cooldown of skills
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

    // shoots bullets
    public void ShootBullet()
    {
        // checks if the cooldown is ready for use or not
        if (checkCooldown(data.bulletCooldownCurrent))
        {
            // set cooldown to the designer defined shooting rate
            data.bulletCooldownCurrent = data.bulletCooldownMax;
            // create a bullet to be fired and save it for use in a moment
            var bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            // use the variable bullet we created a moment ago to get the name of the person who shot it
            bullet.GetComponent<ProjectileData>().shooterName = this.data.myName;

            // Set noiseLevel
            data.noiseLevel = data.bulletNoiseLevel;
            // Play sound
            if (data.bulletFire != null)
            {
                data.soundSource.clip = data.bulletFire;
                data.soundSource.Play();
            }
        }
    }

    // shoots missiles
    // Everything inside works like the ShootBullets() function
    public void ShootMissile()
    {
        if (checkCooldown(data.missileCooldownCurrent))
        {
            data.missileCooldownCurrent = data.missileCooldownMax;
            var missile = Instantiate(missilePrefab, firingPoint.position, firingPoint.rotation);
            missile.GetComponent<ProjectileData>().shooterName = this.data.myName;
            // Play sound
            if (data.missileFire != null)
            {
                data.soundSource.clip = data.missileFire;
                data.soundSource.Play();
            }
            data.noiseLevel = data.missileNoiseLevel;
        }
    }

    // Rotates towards passed in vector
    public void rotateTowards(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, data.rotationSpeed * Time.deltaTime);
        data.noiseLevel = data.rotateNoiseLevel;
    }
}
