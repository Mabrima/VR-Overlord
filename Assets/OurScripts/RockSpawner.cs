﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR.OpenVR;

/* Script Author: Robin Arkblad
 * Edits by:
 */

public class RockSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject rock;
    [SerializeField]
    float timeBetweenRocks = 2;
    [SerializeField]
    int initialSpawn = 3;

    bool spawning = true;


    void Start()
    {

    }

    private IEnumerator SpawnRocks()
    {
        bool initialSpawning = true;
        int spawned = 0;
        while (initialSpawning)
        {
            Instantiate(rock, transform.position, transform.rotation, transform);
            spawned++;
            if (spawned >= initialSpawn)
            {
                initialSpawning = false;
            }
            yield return new WaitForSeconds(1);
        }

        while (spawning)
        {
            Instantiate(rock, transform.position, transform.rotation, transform);
            yield return new WaitForSeconds(timeBetweenRocks);
        }

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            StartCoroutine(SpawnRocks());
        }
    }
}
