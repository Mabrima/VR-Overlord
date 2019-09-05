using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    int damage = 3;
    [SerializeField]
    SphereCollider explosionRadious;
    [SerializeField]
    Transform explosionEffect;

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
            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator Explode()
    {
        SphereCollider colliderSphere = transform.parent.GetComponent<SphereCollider>();
        colliderSphere.enabled = false;
        exploding = true;
        explosionRadious.enabled = true;
        explosionRadious.enabled = false;
        explosionEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(colliderSphere.gameObject);
        yield return null;
    }

}
