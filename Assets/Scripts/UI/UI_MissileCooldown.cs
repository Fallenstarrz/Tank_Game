using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MissileCooldown: MonoBehaviour
{
    public Image missileCD;
    public TankData data;

    // Use this for initialization
    void Start()
    {
        missileCD = GetComponent<Image>();
        data = GetComponentInParent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        // missile cooldown display, the 1- ensure that the fill goes in the correct direction
        missileCD.fillAmount = 1 - (data.missileCooldownCurrent / data.missileCooldownMax);
    }
}
