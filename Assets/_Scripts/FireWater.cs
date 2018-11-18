using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FireWater : MonoBehaviour {
    public static FireWater instance;

    public GameObject waterPartical;
    public Vector3 offSet;
    public float scaleMinValue, scaleMaxValue;
    public GameObject crossArrow;
    bool tapping;
    public GameObject rayCastCollutionDetection;
    public GameObject gunPrefab;
    GameObject currentLevel;
    public int levelNumber;
    public float waterAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelNumber = 0;
    }

    void Update () {
       
        if(Input.GetKey(KeyCode.Mouse0) && GameManager.instance.waterLimit > 0 && GameManager.instance.levelLoaded)
        {
            tapping = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "HittingArea")
                {
                    Invoke("DeactivateRayCastCollider", 0.4f);
                    StartCoroutine(ShootWater(hit));
                }
            }
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)&& GameManager.instance.levelLoaded)
        {
            gunPrefab.transform.DOMoveZ(-13f, 0.2f);
            CancelInvoke("DeactivateRayCastCollider");
            rayCastCollutionDetection.SetActive(true);
            tapping = false;
            crossArrow.gameObject.SetActive(false);
        }
        else
        {
            if (GameManager.instance.waterLimit < 0 && !tapping && !GameManager.instance.gameOver)
            {
                waterAmount = 0.0f;
                GameManager.instance.UpdateWaterIndicator();
                rayCastCollutionDetection.SetActive(false);
            }
        }
	}

    IEnumerator ShootWater(RaycastHit hit)
    {
        for (float i = 0; i < 0.2f; i+=0.05f)
        {
            yield return new WaitForSeconds(0.05f);
            crossArrow.gameObject.SetActive(true);
            crossArrow.transform.position = Input.mousePosition;
            transform.LookAt(hit.point);
            gunPrefab.transform.LookAt(hit.point);
            gunPrefab.transform.DOMoveZ(-13.5f, 0.2f);
            GameObject water = Instantiate(waterPartical);
            ReduceSize(water);
            water.transform.rotation = Quaternion.Euler(new Vector3(-5, 0, Random.Range(0,360)));
            water.transform.position = transform.position;
            water.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
            Destroy(water, 2f);
            if (GameManager.instance.waterLimit > 0)
            {
                GameManager.instance.UpdateWaterIndicator();
                GameManager.instance.waterLimit -= Time.deltaTime;
            }
        }

        gunPrefab.transform.DOMoveZ(-13f, 0.2f);
    }


    void DeactivateRayCastCollider()
    {
        rayCastCollutionDetection.SetActive(false);
    }

    void ReduceSize(GameObject watertoResize)
    {
        if (watertoResize)
        {
            watertoResize.transform.DOScale(0.5f, 1f);
        }
    }

    public void CreateLevel()
    {
        rayCastCollutionDetection.SetActive(true);
    }
}
