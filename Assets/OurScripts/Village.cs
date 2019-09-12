using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script Author: Johan Appelgren
 * Edits by: Philip Åkerblom
 */

public class Village : MonoBehaviour
{
    public GameObject LoseText;
    UnitHealth health;

    private void Start()
    {
        LoseText.SetActive(false);
        health = GetComponent<UnitHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health.TakeDamage(other.GetComponent<Enemy>().damage);
            other.GetComponent<UnitHealth>()?.TakeDamage(50);

            if (health.health <= 0)
                GameOver();
        }
    }

    void GameOver()
    {
        LoseText.SetActive(true);
    }
}
