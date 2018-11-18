using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimal : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
    }
}
