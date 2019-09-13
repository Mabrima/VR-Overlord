using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Script Author: Robin Arkblad
 * Edits by: Johan Appelgren
 */

public class Enemy : MonoBehaviour
{

    [SerializeField] float speed = 0.1f;
    public int damage;

    Animator animator;
    UnitHealth health;
    NavMeshAgent agent;
    GameObject village;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        village = GameObject.FindGameObjectWithTag("Village");
        health = GetComponent<UnitHealth>();
        animator = GetComponent<Animator>();
        StartCoroutine("Navigation");
        agent.speed = speed;
    }

    public void TakingDamage()
    {
        if (health.health <= 0)
            StartCoroutine(Dying());
        else
            animator.SetTrigger("Hit");
    }

    IEnumerator Navigation()
    {
        while (health.health > 0)
        {
            agent.SetDestination(village.transform.position);

            yield return new WaitForSeconds(.5f);
        }
        agent.SetDestination(transform.position);
    }

    IEnumerator Dying()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
