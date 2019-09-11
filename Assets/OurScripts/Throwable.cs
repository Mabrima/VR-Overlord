using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Robin Arkblad 
 * Edits by:
 */

public class Throwable : MonoBehaviour
{
    [SerializeField]
    int health = 1;
    [SerializeField]
    int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<UnitHealth>()?.TakeDamage(damage);

        if (other.CompareTag("Enemy"))
        {
            health--;
            if (health <= 0)
                Destroy(gameObject);
        }

    }
}
