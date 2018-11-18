using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedObjectDetection : MonoBehaviour 
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Cube")
        {
            LevelSetUp.instance.RemoveKnockedObjects(other.gameObject);
        }
    }
}
