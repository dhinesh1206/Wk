using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveClouds : MonoBehaviour {


	// Use this for initialization
	void Start () {
        int traveltime = Random.Range(20, 50);
        transform.DOMoveX(-transform.position.x, traveltime).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
	}
}
