using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotor : MonoBehaviour
{
    [HideInInspector] public ProjectileData data;
    [HideInInspector] public Rigidbody rb;

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
            // Debug.Log("Enemy Tank Hit"); // Used in testing successful collisions with other tanks
            other.gameObject.GetComponent<TankHealth>().reduceCurrentHealth(data.projectileDamage);
        }
        // destroy the projectile
        // Debug.Log("Collision Detected!"); // Used in testing successful collisions
        Destroy(this.gameObject);
    }

    // Propels a projectile forward
    public void pushForward()
    {
            rb.AddForce(transform.up * data.projectileSpeed);
    }
}
