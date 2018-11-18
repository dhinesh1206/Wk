using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGameobjectsOnHit : MonoBehaviour {

    public List<string> collisionTag;
    public bool changeRigidBody,isKinematic;



    private void OnCollisionEnter(Collision collision)
    {
        if (collisionTag.Contains(collision.gameObject.tag))
        {
            gameObject.transform.SetParent(null);
            if(changeRigidBody)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = isKinematic;
            }
        }
    }
}
