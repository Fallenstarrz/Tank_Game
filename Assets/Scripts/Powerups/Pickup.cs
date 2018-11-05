using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PowerupHealth healthPowerup;
    public PowerupSpeed speedPowerup;
    public PowerupMissileCD missileCDPowerup;



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
    }

    public void OnTriggerEnter(Collider other)
    {
        // pick a powerup type
        Controller_Powerup buffController = other.GetComponent<Controller_Powerup>();
        if (buffController != null)
        {
            switch (currentPowerupType)
            {
                case PowerupType.powerupHealth:
                {
                    buffController.add(healthPowerup);
                    break;
                }
                case PowerupType.powerupSpeed:
                {
                    buffController.add(speedPowerup);
                    break;
                }
                case PowerupType.powerupMissileCDReduction:
                {
                    buffController.add(missileCDPowerup);
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
