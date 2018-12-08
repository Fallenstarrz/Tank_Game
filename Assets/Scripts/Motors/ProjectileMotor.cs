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
        //pushForward(); // Used if using rb to propel projectiles
    }

    // remove update if using rb to propel projectiles
    public void Update()
    {
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
            TankHealth targetHit = other.gameObject.GetComponent<TankHealth>();
            // if it is a player or an enemy tank then reduce their health
            targetHit.reduceCurrentHealth(data.projectileDamage, data.shooterName);
        }
        // Create Particle Effect
        // Destroy Particle Effect After period of time
        spawnParticleEffect();
        if (data.hitSound != null)
        {
            AudioSource.PlayClipAtPoint(data.hitSound, transform.position);
        }
        // destroy the projectile
        Destroy(this.gameObject);
    }

    // spawns particle effects and destroys them after a delay
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
        //rb.AddForce(transform.forward * data.projectileSpeed); // used if using rb to propel projectiles
        transform.Translate(Vector3.forward * data.projectileSpeed * Time.deltaTime);
    }
}
