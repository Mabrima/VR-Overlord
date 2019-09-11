using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Johan Appelgren
 * Edits by:
 */

public class UnitHealth : MonoBehaviour
{

    [SerializeField] public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = health <= 0 ? 0 : health;

        GetComponent<Enemy>()?.TakingDamage();
    }
}
