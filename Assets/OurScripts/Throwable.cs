using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Robin Arkblad 
 * Edits by:
 */

public class Throwable : MonoBehaviour
{
    [SerializeField]
    int damage = 1;
    bool removeRoutineRunning = false;

    private void OnEnable()
    {
        removeRoutineRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<UnitHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
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
        gameObject.SetActive(false);
        yield return null;
    }
}
