using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHealth : MonoBehaviour {
    
    public float fireHealth = 0.01f;
    public List<GameObject> collidedObjects;
    public bool inFire;
    Rigidbody rb;
    public GameObject part;
    public bool startatingFire;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if (inFire && !rb.isKinematic)
        {
            fireHealth += Time.deltaTime;

            if(part)
            {
                part.GetComponent<ParticleSystem>().startLifetime -= Time.deltaTime / 10;
            }
        }
        if (startatingFire)
        {
            Invoke("StartFire", 3f);
        }
        else
        {
            if (fireHealth >= 6f && fireHealth <7)
            {
                foreach (var item in collidedObjects)
                {
                    if (item)
                    {
                        if (!item.GetComponent<FireHealth>().inFire)
                        {

                            for (int i = 0; i < item.transform.childCount; i++)
                            {
                                Destroy(item.transform.GetChild(0).gameObject);
                            }
                            GameObject obj = Instantiate(GameManager.instance.firePartical, item.transform);
                            item.GetComponent<FireHealth>().part = obj;
                            item.GetComponent<FireHealth>().inFire = true;

                        }
                    }

                }
               
            } else if(fireHealth >= 7f)
            {
                gameObject.SetActive(false);
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            if (fireHealth >= 0.9f)
            {
                
            }
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (!collidedObjects.Contains(collision.contacts[i].otherCollider.gameObject))
                {
                    collidedObjects.Add(collision.contacts[i].otherCollider.gameObject);
                }
            }
        }
    }

    void StartFire()
    {
        CancelInvoke();
        foreach (var item in collidedObjects)
        {
            if (item)
            {
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    Destroy(item.transform.GetChild(0).gameObject);
                }
                GameObject obj = Instantiate(GameManager.instance.firePartical, item.transform);
                item.GetComponent<FireHealth>().part = obj;
                item.GetComponent<FireHealth>().inFire = true;
            }
        }
        gameObject.SetActive(false);
    }
}
