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

    //On enable make make sure the bool for removal is set to false
    private void OnEnable()
    {
        removeRoutineRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        //If it connects with an enemy deal damage and dissapear.
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<UnitHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        //if it connects with terrain or the floor, start the coroutine to remove it after a short while.
        else if (other.CompareTag("Terrain") || other.CompareTag("Floor"))
        {
            if (!removeRoutineRunning)
            {
                removeRoutineRunning = true;
                StartCoroutine(RemoveAfterTime());
            }
        }

    }

    //Removes the rock from the scene after a short duration.
    IEnumerator RemoveAfterTime()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
        yield return null;
    }
}
