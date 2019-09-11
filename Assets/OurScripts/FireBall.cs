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
    GameObject navMeshObstacle;

    bool exploding = false;

    // Start is called before the first frame update
    void Start()
    {
        explosionRadious.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!exploding && (other.tag == "Enemy" || other.tag == "Terrain"))
        {
            StartCoroutine(Explode());
            return;
        }
        else
        {
            other.GetComponent<UnitHealth>()?.TakeDamage(damage);
        }

    }

    private IEnumerator Explode()
    {
        SphereCollider colliderSphere = transform.parent.GetComponent<SphereCollider>();
        colliderSphere.enabled = false;
        exploding = true;
        explosionRadious.enabled = true;
        explosionRadious.enabled = false;
        yield return new WaitForSeconds(.1f);
        GameObject tempNMO = Instantiate(navMeshObstacle, transform.position, Quaternion.identity);
        GameObject tempExplosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(tempExplosion);
        //Create burn effect
        yield return new WaitForSeconds(19);
        //Destroy burn effect
        Destroy(tempNMO);
        Destroy(transform.parent.gameObject);
        yield return null;
    }



}
