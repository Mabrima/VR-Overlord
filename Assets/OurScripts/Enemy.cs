﻿using System.Collections;
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
    CapsuleCollider collider;
    GameObject village;
    bool dying = false;
    Vector3 spawnPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<UnitHealth>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        agent.speed = speed;
        village = GameObject.FindGameObjectWithTag("Village");
        StartCoroutine(Navigation());
    }

    //Take damage, if you have no health left, die.
    public void TakingDamage()
    {
        if (health.health > 0)
            animator.SetTrigger("Hit");
        else if (!dying)
            StartCoroutine(Dying());
    }

    //Navigates the AI towards the goal.
    IEnumerator Navigation()
    {
        while (health.health > 0)
        {
            agent.SetDestination(village.transform.position);

            yield return new WaitForSeconds(.5f);
        }
        //if dead stop moving
        agent.SetDestination(transform.position);
    }

    //On death tell spawnmanager that you have died and animate you dying.
    IEnumerator Dying()
    {
        dying = true;
        collider.enabled = false;
        SpawnManager.instance.EnemyDefeated();
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    //Reset to be used.
    public void Reset()
    {
        animator.SetBool("Death", false);
        dying = false;
        collider.enabled = true;
        health.ResetHealth();
        SetPosition(spawnPosition);
        StartCoroutine(Navigation());
    }

    //Sets where they should be spawned
    public void SetSpawnPosition(Vector3 position)
    {
        spawnPosition = position;
    }

    //Sets their current position. Needs to use agent.Warp to work properly.
    private void SetPosition(Vector3 position)
    {
        agent.Warp(position);
    }
}
