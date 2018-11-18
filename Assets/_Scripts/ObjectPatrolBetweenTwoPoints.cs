using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectPatrolBetweenTwoPoints : MonoBehaviour 
{
    public Vector3 pointA, pointB;
    public float timeDifferencefromPointAtoB, timeDifferencefromPointBtoA;
    public float initialstartDelay, delayfromPointAtoB, delayfromPointBtoA;
    public Ease easeType = Ease.Linear;
    public bool patrol = true;
    public bool loop = true;
	
	void Start () {
        Invoke("ActivateDelay", initialstartDelay);
	}

    void ActivateDelay()
    {
        StartCoroutine(StartMovingAtoB());
    }

    IEnumerator StartMovingAtoB()
    {
        yield return new WaitForSeconds(delayfromPointAtoB);
        transform.DOLocalMove(pointB, timeDifferencefromPointAtoB, false).SetEase(easeType).OnComplete(() =>
        {
            if (patrol)
            {
                StartCoroutine(StartMovingBtoA());
            }
        });
    }
    IEnumerator StartMovingBtoA()
    {
        yield return new WaitForSeconds(delayfromPointBtoA);
        transform.DOLocalMove(pointA, timeDifferencefromPointBtoA, false).SetEase(easeType).OnComplete(() =>
        {
            if (loop)
            {
                StartCoroutine(StartMovingAtoB());
            }
        });
    }
}
