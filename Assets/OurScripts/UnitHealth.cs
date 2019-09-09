using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    /* Script Author: Johan Appelgren
     * Edits by:
     */

    [SerializeField] public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = health <= 0 ? 0 : health - damage;

        GetComponent<Enemy>()?.TakingDamage();
    }
}
