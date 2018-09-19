using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotor : MonoBehaviour
{
    public TankData data;
    public Rigidbody rb;

    // Use this for initialization
    private void Start()
    {
        data = GetComponent<TankData>();
        if (data == null)
        {
            Debug.Log("DataIsNull");
        }
        rb = GetComponent<Rigidbody>();

        destroyAfter();
        pushForward();
    }
    public void destroyAfter()
    {
        if (gameObject.name == "Bullet")
        {
            Destroy(this.gameObject, data.bulletDestroyTimer);
        }
        else
        {
            Destroy(this.gameObject, data.missileDestroyTimer);
        }
    }
    public void pushForward()
    {
        if (gameObject.name == "Bullet")
        {
            rb.AddForce(Vector3.forward * data.bulletSpeed);
        }
        else
        {
            rb.AddForce(Vector3.forward* data.missileSpeed);
        }
    }
}
