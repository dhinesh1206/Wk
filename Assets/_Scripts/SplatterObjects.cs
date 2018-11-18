using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplatterObjects : MonoBehaviour {

    public GameObject splatterObjects;
    public GameObject cubeObjects;
    public bool splatterGlass;
    public float scaleTime;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            cubeObjects.SetActive(false);
            splatterObjects.SetActive(true);
            LevelSetUp.instance.RemoveKnockedObjects(other.gameObject);
            if(splatterGlass)
            {
                for (int i = 0; i < splatterObjects.transform.childCount; i++)
                {
                    splatterObjects.transform.DOScale(0,scaleTime);
                }
            }
        }
    }
}
