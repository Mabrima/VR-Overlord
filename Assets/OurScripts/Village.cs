using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Johan Appelgren
 * Edits by:
 */

public class Village : MonoBehaviour
{

    UnitHealth health;

    private void Start()
    {
        health = GetComponent<UnitHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health.TakeDamage(1);
            other.GetComponent<UnitHealth>()?.TakeDamage(50);
        }
    }
}
