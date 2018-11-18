using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassManager : MonoBehaviour {

    public GameManager.MassTypes massTypes = GameManager.MassTypes.Medium;

	// Use this for initialization
	void Start () {
        if(massTypes == GameManager.MassTypes.VeryLarge)
        {
            gameObject.GetComponent<Rigidbody>().mass = GameManager.instance.massManagerList[0];
        } 
        else if(massTypes == GameManager.MassTypes.Large)
        {
            gameObject.GetComponent<Rigidbody>().mass = GameManager.instance.massManagerList[1];
        }
        else if (massTypes == GameManager.MassTypes.Medium)
        {
            gameObject.GetComponent<Rigidbody>().mass = GameManager.instance.massManagerList[2];
        }
        else if (massTypes == GameManager.MassTypes.Small)
        {
            gameObject.GetComponent<Rigidbody>().mass = GameManager.instance.massManagerList[3];
        }
        else if (massTypes == GameManager.MassTypes.VerySmall)
        {
            gameObject.GetComponent<Rigidbody>().mass = GameManager.instance.massManagerList[4];
        }
	}
}
