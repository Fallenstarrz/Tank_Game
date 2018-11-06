using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Powerup types
    public PowerupHealth healthPowerup;
    public PowerupSpeed speedPowerup;
    public PowerupMissileCD missileCDPowerup;

    // Particle Effects
    public GameObject healthEffect;
    public GameObject speedEffect;
    public GameObject missileCDEffect;
    public Vector3 offset;
    public float destroyDelay;

    // Icons
    public Material[] mats;
    public Material healthIcon;
    public Material speedIcon;
    public Material missileCDIcon;

    public enum PowerupType
    {
        powerupHealth,
        powerupSpeed,
        powerupMissileCDReduction
    }
    public PowerupType currentPowerupType;

    // TODO: Choose a soundclip
    public AudioClip pickupSound;

    public void Start()
    {
        // choose a new powerup
        currentPowerupType = (PowerupType)UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(PowerupType)).Length);
        mats = GetComponent<MeshRenderer>().materials;
        // Change material
        switch (currentPowerupType)
        {
            case PowerupType.powerupHealth:
            {
                name = "Health Powerup";
                mats[0] = healthIcon;
                break;
            }
            case PowerupType.powerupSpeed:
            {
                name = "Speed Powerup";
                mats[0] = speedIcon;
                break;
            }
            case PowerupType.powerupMissileCDReduction:
            {
                name = "Missile CD Powerup";
                mats[0] = missileCDIcon;
                break;
            }
        }
        GetComponent<MeshRenderer>().materials = mats;
    }

    public void OnTriggerEnter(Collider other)
    {
        // pick a powerup type
        // add powerup to list
        // spawn particle effect
        Controller_Powerup buffController = other.GetComponent<Controller_Powerup>();
        if (buffController != null)
        {
            switch (currentPowerupType)
            {
                case PowerupType.powerupHealth:
                {
                    buffController.add(healthPowerup);
                    GameObject tempEffect = Instantiate(healthEffect, transform.position + offset, healthEffect.transform.rotation);
                    Destroy(tempEffect, destroyDelay);
                    break;
                }
                case PowerupType.powerupSpeed:
                {
                    buffController.add(speedPowerup);
                    GameObject tempEffect = Instantiate(speedEffect, transform.position + offset, speedEffect.transform.rotation);
                    Destroy(tempEffect, destroyDelay);
                    break;
                }
                case PowerupType.powerupMissileCDReduction:
                {
                    buffController.add(missileCDPowerup);
                    GameObject tempEffect = Instantiate(missileCDEffect, transform.position + offset, missileCDEffect.transform.rotation);
                    Destroy(tempEffect, destroyDelay);
                    break;
                }
            }
            // if sound is set play it
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1.0f);
            }
            // Destroy gameObject
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        GameManager.instance.spawnedPickups.Remove(this.gameObject);
    }
}
