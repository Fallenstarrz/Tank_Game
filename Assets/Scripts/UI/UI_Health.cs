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
	    data = GetComponent<TankData>();
	    healthBar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        healthBar.value = data.healthCurrent / data.healthMax;
    }
}
