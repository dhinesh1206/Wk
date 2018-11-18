using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAlphaRequceonHit : MonoBehaviour
{
    [SerializeField] float hitCount;
    [SerializeField] GameObject componentToActivate;
    [SerializeField] GameObject birdAnimation;
    [SerializeField] GameObject objectMaterial;
    [SerializeField] Material[] crackMaterial;
    bool collided;
    [SerializeField] bool explode;
    Material[] objmaterial;
    int hitTaken;
    private void Start()
    {
        objmaterial = objectMaterial.GetComponent<Renderer>().materials;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water" && !collided)
        {
            Collider[] collidersAround = Physics.OverlapSphere(collision.contacts[0].point, 3);
            foreach (Collider item in collidersAround)
            {
                Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();
                print(item.gameObject.name);
                if (rb != null)
                    rb.AddExplosionForce(5000, collision.contacts[0].point, 300);
            }
            collided = true;
            Invoke("ColliderOn", 0.5f);
            Material[] materialColor = objmaterial;
            hitTaken += 1;
            double percent = (hitTaken / (hitCount));

            if (percent <= 0.3f && percent > 0f)
            {
                materialColor[0] = crackMaterial[0];
            }
            else if (percent <= 0.6f && percent > 0.3f)
            {
                materialColor[0] = crackMaterial[1];
            }
            else if (percent <= 1f && percent > 0.6f)
            {
                print(percent);
                materialColor[0] = crackMaterial[2];
            }
            objectMaterial.GetComponent<Renderer>().materials = materialColor;
            if (hitTaken == hitCount)
            {
                componentToActivate.gameObject.SetActive(true);
                componentToActivate.transform.SetParent(null);
                birdAnimation.SetActive(true);
                objectMaterial.SetActive(false);
                transform.GetComponent<Collider>().enabled = false;
            }
        }
    }

    void ColliderOn()
    {
        collided = false;
    }

}