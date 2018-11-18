using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFallingObjects : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cube")
        {
            LevelSetUp.instance.RemoveKnockedObjects(collision.gameObject);
        }
    }
}
