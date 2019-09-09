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
    int health = 3;
    [SerializeField]
    int hits = 0;
    [SerializeField]
    float speed = 0.0001f;
    [SerializeField]
    TextMesh counter;

    Animator animator;

    NavMeshAgent agent;
    GameObject box;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        box = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;
        agent.SetDestination(box.transform.position);
    }

    public void TakeDamage(int damage)
    {
        if (counter != null)
        {
            hits++;
            counter.text = "" + hits;
        }
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Dying());
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    IEnumerator Dying()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        yield return null;
    }
}
