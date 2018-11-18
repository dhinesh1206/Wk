using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float radius = 5f;
    public float power = 10.0F;

    // Use this for initialization
    void Start()
    {
        Collider[] collidersAround = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider item in collidersAround)
        {
            Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power, transform.position, radius);
        }
    }
}