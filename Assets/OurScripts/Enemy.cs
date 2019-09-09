using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /* Script Author: 
     * Edits by: Johan Appelgren
     */

    [SerializeField]
    int hits = 0;
    [SerializeField]
    float speed = 0.1f;

    Animator animator;
    UnitHealth unitHealth;
    NavMeshAgent agent;
    GameObject box;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        box = GameObject.FindGameObjectWithTag("Player");
        unitHealth = GetComponent<UnitHealth>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;
        if (box != null)
            agent.SetDestination(box.transform.position);
    }

    public void TakingDamage()
    {
        if (unitHealth.health <= 0)
            StartCoroutine(Dying());
        else
            animator.SetTrigger("Hit");
    }

    IEnumerator Dying()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        yield return null;
    }
}
