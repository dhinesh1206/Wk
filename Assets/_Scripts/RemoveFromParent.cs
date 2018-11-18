using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFromParent : MonoBehaviour {

    public string childTag;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == childTag)
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
