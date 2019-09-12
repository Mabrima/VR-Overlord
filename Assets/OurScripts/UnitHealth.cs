using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Johan Appelgren
 * Edits by:
 */

public class UnitHealth : MonoBehaviour
{
    public int health;
    int startingHealth;

    void Start()
    {
        startingHealth = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = health <= 0 ? 0 : health;

        GetComponent<Enemy>()?.TakingDamage();
    }

    public void ResetHealth()
    {
        health = startingHealth;
    }
}
