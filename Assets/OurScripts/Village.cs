using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script Author: Johan Appelgren
 * Edits by: Philip Åkerblom
 */

public class Village : MonoBehaviour
{
    UnitHealth health;
    VillageTextController text;

    private void Start()
    {
        health = GetComponent<UnitHealth>();
        text = GetComponent<VillageTextController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health.TakeDamage(other.GetComponent<Enemy>().damage);
            other.GetComponent<UnitHealth>().TakeDamage(50);

            if (health.health <= 0)
            {
                text.GameOver();
                SpawnManager.instance.TurnOffAllSpawnedObjects();
            }
        }
    }

    public void Reset()
    {
        health.ResetHealth();
        text.ResetText();
    }
}
