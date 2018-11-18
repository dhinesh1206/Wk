using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionOnShapesCollusion : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cube")
        {
            Collider[] collidersAround = Physics.OverlapSphere(transform.position, 3.5f);

            foreach (Collider item in collidersAround)
            {
                if (item.gameObject.tag == "Cube")
                {
                    Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.AddExplosionForce(5000, transform.position, 3.5f);
                }
            }
        }
    }
}
