using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFromLevelSetup : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cube")
        {
            StartCoroutine(CallReMoveObject(other.gameObject));
        }
    }

    IEnumerator CallReMoveObject(GameObject other)
    {
        yield return new WaitForSeconds(1f);
        if(other!=null)
        LevelSetUp.instance.RemoveKnockedObjects(other);
    }
}
