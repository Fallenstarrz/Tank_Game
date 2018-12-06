using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    // Projectile statistics
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileLifespan;

    // Sound Info
    public AudioClip hitSound;

    // This variable holds the name of the person who shot it
    // TODO: Currently no way to set the name of the tank, so we don't use it quite yet, but we do track it
    public TankData shooterName;
}
