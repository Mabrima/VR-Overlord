using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Johan Appelgren
 * Edits by:
 */

public class UnitHealth : MonoBehaviour
{
    [SerializeField] int startingHealth;
    public int health;

    void Start()
    {
        health = startingHealth;
    }

    //Take incoming damage and apply it to own health
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = health <= 0 ? 0 : health;

        //animation
        GetComponent<Enemy>()?.TakingDamage();
    }

    //Sets own health to starting value
    public void ResetHealth()
    {
        health = startingHealth;
    }
}
