using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    // Player Variables
    // TODO: Need to make a way for the player to set their tanks name and randomly generate names for AI enemies
    public string myName = "No Name";

    // Tank Variables
    public float movementSpeed;
    public float backwardMovementSpeed;
    public float rotationSpeed;
    public float healthCurrent;
    public float healthMax;

    // Bullet Variables
    public float bulletCooldownMax;
    public float bulletCooldownCurrent;


    // Missile Variables
    public float missileCooldownMax;
    public float missileCooldownCurrent;


}
