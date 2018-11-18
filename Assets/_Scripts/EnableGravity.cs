using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGravity : MonoBehaviour {

    public string colliderTag;
    public Rigidbody[] objectsToActivate;
    bool activated;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == colliderTag && !activated)
        {
            activated = true;
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].useGravity = true;
            }
        }
    }

}
