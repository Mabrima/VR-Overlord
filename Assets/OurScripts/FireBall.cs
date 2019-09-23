using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Author: Robin Arkblad

public class FireBall : MonoBehaviour
{
    [SerializeField]
    int damage = 3;
    [SerializeField]
    SphereCollider explosionRadious;
    [SerializeField]
    GameObject explosionEffect;
    [SerializeField]
    GameObject fireEffect;
    [SerializeField]
    GameObject navMeshObstacle;

    bool exploding = false;

    // Start is called before the first frame update
    void Start()
    {
        explosionRadious.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if not already exploding and hits terrain or enemy start the Explode coroutine.
        if (!exploding && (other.CompareTag("Enemy") || other.CompareTag("Terrain")))
        {
            StartCoroutine(Explode());
            return;
        }
        else if (other.CompareTag("Floor"))
        {
            transform.parent.gameObject.SetActive(false);
        }
        //if exploding and it has a health script deal damage.
        else
        {
            other.GetComponent<UnitHealth>()?.TakeDamage(damage);
        }
    }

    //On disable (level reset mainly) make sure to clean up behind you first, and reset to be ready to be used again.
    private void OnDisable()
    {
        if (exploding)
        {
            Destroy(tempExplosion);
            Destroy(tempFire);
            Destroy(tempNMO);
            colliderSphere.enabled = true;
            exploding = false;
        }
    }

    GameObject tempNMO;
    GameObject tempExplosion;
    GameObject tempFire;
    SphereCollider colliderSphere;

    //Does everything that needs to be done during an explosion.
    private IEnumerator Explode()
    {
        //Bool to keep track of which state we are in.
        exploding = true;
        //Makes it keep falling through so it "dissapears"
        colliderSphere = transform.parent.GetComponent<SphereCollider>();
        colliderSphere.enabled = false;
        //Catches everything in it's explosion radious to be handled.
        explosionRadious.enabled = true;
        explosionRadious.enabled = false;
        //Spawn everything that needs to be seen/used to make the explosion
        tempNMO = Instantiate(navMeshObstacle, transform.position, Quaternion.identity);
        tempExplosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        tempFire = Instantiate(fireEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        //After a short while remove the explosion effect.
        Destroy(tempExplosion);
        yield return new WaitForSeconds(19);
        //At the end remove the fire effect and the navmeshblocker.
        Destroy(tempFire);
        Destroy(tempNMO);
        //reset and remove the object.
        colliderSphere.enabled = true;
        exploding = false;
        transform.parent.gameObject.SetActive(false);
        yield return null;
    }



}
