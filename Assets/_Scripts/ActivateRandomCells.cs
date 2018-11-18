using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomCells : MonoBehaviour {

    public GameObject[] cells;

	// Use this for initialization
	void Start () 
    {
        foreach (var cell in cells)
        {
            int random = Random.Range(0, 3);
            if(random == 0)
            {
                cell.SetActive(false);
            } else {
                cell.SetActive(true);
            }
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}
