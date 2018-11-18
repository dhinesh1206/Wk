using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingBossBehaviour : MonoBehaviour 
{
    public float limitX, limitY, minTime, maxTime;

	void Start () {
        SetTargetPosition();
	}

    public void SetTargetPosition(){
        Vector3 newTarget = new Vector3(Random.Range(-limitX,limitX),Random.Range(-1.0f,10.0f),transform.position.z);
        transform.DOMove(newTarget, Random.Range(minTime, maxTime), false).OnComplete(() =>
        {
            SetTargetPosition();
        });
    }
}
