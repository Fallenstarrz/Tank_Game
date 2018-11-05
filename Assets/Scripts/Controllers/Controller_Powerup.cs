using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Powerup : MonoBehaviour
{
    public TankData data;
    public List<Powerup> buffs;
    public List<Powerup> expiredBuffs;

	// Use this for initialization
	void Start ()
    {
        buffs = new List<Powerup>();
        data = GetComponent<TankData>();
        expiredBuffs = new List<Powerup>();
	}

    public void add(Powerup powerup)
    {
        powerup.OnActivated(data);
        powerup.buffDurationCurrent = powerup.buffDurationMax;
        if (powerup.isPerm == false)
        {
            buffs.Add(powerup);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        foreach (Powerup buff in buffs)
        {
            buff.buffDurationCurrent -= Time.deltaTime;
            if (buff.buffDurationCurrent <= 0)
            {
                expiredBuffs.Add(buff);
            }
        }
        foreach (Powerup expiredBuff in expiredBuffs)
        {
            buffs.Remove(expiredBuff);
            expiredBuff.OnDeactivated(data);
        }
        expiredBuffs.Clear();
	}

}
