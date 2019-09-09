using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Author: Robin

public class Throwable : MonoBehaviour
{
    [SerializeField]
    int health = 1;
    [SerializeField]
    int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
