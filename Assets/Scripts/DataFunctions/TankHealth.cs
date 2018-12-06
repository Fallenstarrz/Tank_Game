using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    [HideInInspector] public TankData data;

	// Use this for initialization
	void Start ()
    {
        data = GetComponent<TankData>();
	}

    // reduce current health by the damage of the thing that hit it
    public void reduceCurrentHealth(float damage, TankData attacker)
    {
        data.healthCurrent -= damage;
        checkDeath(attacker);
    }

    // check if player has died
    public void checkDeath(TankData damageDealer)
    {
        if (data.healthCurrent <= 0)
        {
            damageDealer.score += GameManager.instance.scorePerKill;
            Destroy(this.gameObject);
        }
    }

    // Reset current health to max health
    public void resetHealth()
    {
        data.healthCurrent = data.healthMax;
    }

}
