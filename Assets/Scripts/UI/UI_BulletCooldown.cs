using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BulletCooldown : MonoBehaviour
{
    public Image bulletCD;
    public TankData data;

	// Use this for initialization
	void Start ()
    {
        bulletCD = GetComponent<Image>();
        data = GetComponentInParent<TankData>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // controls the UI bullet CD display, the 1- ensures that it fills in the correct direction
        bulletCD.fillAmount = 1 - (data.bulletCooldownCurrent / data.bulletCooldownMax);
	}
}
