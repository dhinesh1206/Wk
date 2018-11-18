using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreateClouds : MonoBehaviour {

    public GameObject[] clouds;
    public float sizeminimum, sizeMaximum;
    public float fromPosition;
    public float ToPosition;


	// Use this for initialization
	void Start () {
        StartCreatingClouds();
        MoveSpawner();
	}
	
    void StartCreatingClouds()
    {
        GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length)],transform);
        cloud.transform.SetParent(null);
       // cloud.GetComponent<SpriteRenderer>().color = LevelSetUp.instance.cloudColor;
        cloud.transform.localScale = Vector3.one * Random.Range(sizeMaximum, sizeminimum);
        float timeForNextCloud = Random.Range(15, 10);
        Invoke("StartCreatingClouds", timeForNextCloud);
    }

    void MoveSpawner()
    {
        float newPosition = Random.Range(fromPosition, ToPosition);
        transform.DOMoveY(newPosition, 0.5f, false).OnComplete(() =>
        {
            MoveSpawner();
        });
    }

}
