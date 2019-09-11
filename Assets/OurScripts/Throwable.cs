﻿using System.Collections;
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

    bool removeRoutineRunning = false;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<UnitHealth>()?.TakeDamage(damage);

        if (other.CompareTag("Enemy"))
        {
            health--;
            if (health <= 0)
                Destroy(gameObject);
        }
        else if (other.CompareTag("Terrain"))
        {
            if (!removeRoutineRunning)
            {
                removeRoutineRunning = true;
                StartCoroutine(RemoveAfterTime());
            }
        }

    }

    IEnumerator RemoveAfterTime()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
        yield return null;
    }
}
