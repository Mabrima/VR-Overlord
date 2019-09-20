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

    private IEnumerator Explode()
    {
        colliderSphere = transform.parent.GetComponent<SphereCollider>();
        colliderSphere.enabled = false;
        exploding = true;
        explosionRadious.enabled = true;
        explosionRadious.enabled = false;
        yield return new WaitForSeconds(.1f);
        tempNMO = Instantiate(navMeshObstacle, transform.position, Quaternion.identity);
        tempExplosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        tempFire = Instantiate(fireEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(tempExplosion);
        yield return new WaitForSeconds(19);
        Destroy(tempFire);
        Destroy(tempNMO);
        colliderSphere.enabled = true;
        transform.parent.gameObject.SetActive(false);
        exploding = false;
        yield return null;
    }



}
