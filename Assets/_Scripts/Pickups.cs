using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour {

    public GameManager.PickUpEnum pickUpType;
    public float explosionRadius;
    public float explosionPower;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            if(pickUpType == GameManager.PickUpEnum.Bomb)
            {
                GameManager.instance.PlayBombSound();
                Collider[] collidersAround = Physics.OverlapSphere(transform.position, explosionRadius);
                GameObject explositionPartical = Instantiate(GameManager.instance.explositionPartical);
                explositionPartical.transform.position = collision.contacts[0].point;
                explositionPartical.GetComponent<ParticleSystem>().Play();
                foreach (Collider item in collidersAround)
                {
                    Rigidbody rb = item.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.AddExplosionForce(explosionPower, transform.position,explosionRadius);
                }
                Destroy(gameObject);
            }
            else if(pickUpType == GameManager.PickUpEnum.Water)
            {
                GameManager.instance.WaterPickUp(collision.contacts[0].point);
                Destroy(gameObject);
            }
        }
    }
}
