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
        data = GetComponent<ProjectileData>();
        rb = GetComponent<Rigidbody>();

        Destroy(this.gameObject, data.projectileLifespan);

        pushForward();
    }
    public void pushForward()
    {
            rb.AddForce(transform.up * data.projectileSpeed);
    }
}
