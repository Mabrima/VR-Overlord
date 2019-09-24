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
    [SerializeField] Slider slider;

    private void Start()
    {
        health = GetComponent<UnitHealth>();
        text = GetComponent<VillageTextController>();
        slider.maxValue = health.GetStartingHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health.TakeDamage(other.GetComponent<Enemy>().damage);
            slider.value = health.GetStartingHealth() - health.health;
            other.GetComponent<UnitHealth>().TakeDamage(50);

            if (health.health <= 0)
            {
                text.GameOver();
                SpawnManager.instance.OnLose();
            }
        }
    }

    public void Reset()
    {
        health.ResetHealth();
        text.ResetText();
    }
}
