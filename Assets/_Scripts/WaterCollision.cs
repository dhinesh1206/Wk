using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour 
{ 
    public GameObject waterParticle,waterParticle1;
    public float particleDelay;
    private void Awake()
    {
        Invoke("ShowParticle", particleDelay);
    }

    void ShowParticle(){
        waterParticle.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.tag == "Cube")
        {
            GameObject water = Instantiate(waterParticle1);
            water.transform.position = collision.contacts[0].point;
        }
    }
}
