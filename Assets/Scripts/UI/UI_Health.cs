using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    public TankData data;
    public Slider healthBar;

	// Use this for initialization
	void Start ()
	{
	    data = GetComponentInParent<TankData>();
	    healthBar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        // set UI healthbar value to current/max health. This gives us a % to fill our bar with
        healthBar.value = data.healthCurrent / data.healthMax;
    }
}
