using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotor : MonoBehaviour
{
    [HideInInspector] public ProjectileData data;
    [HideInInspector] public Rigidbody rb;

    public GameObject explosionEffect;
    public float deleteDelay;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        data = GetComponent<ProjectileData>();

        // Destroy this projectile after X amount of time from spawn
        Destroy(this.gameObject, data.projectileLifespan);
        pushForward();
    }

    // When a projectile collides with something
    // We used triggers here so the rigidbodies don't interact in undesirable ways
    private void OnTriggerEnter(Collider other)
    {
        // check to see if the target his is either a player tank or an enemy tank
        if (GameManager.instance.players.Contains(other.gameObject.GetComponent<TankData>()) ||
            GameManager.instance.aiUnits.Contains(other.gameObject.GetComponent<TankData>()))
        {
            // if it is a player or an enemy tank then reduce their health
            other.gameObject.GetComponent<TankHealth>().reduceCurrentHealth(data.projectileDamage);
        }
        // Create Particle Effect
        // Destroy Particle Effect After period of time
        spawnParticleEffect();
        // destroy the projectile
        Destroy(this.gameObject);
    }

    public void spawnParticleEffect()
    {
        if (explosionEffect != null)
        {
            GameObject tempEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(tempEffect, deleteDelay);
        }
    }

    // Propels a projectile forward
    public void pushForward()
    {
            rb.AddForce(transform.forward * data.projectileSpeed);
    }
}
