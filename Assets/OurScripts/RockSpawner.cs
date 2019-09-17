using System.Collections;
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
    [SerializeField]
    TextMesh text;

    int availableRocks = 0;

    public bool spawning = true;


    void Start()
    {
        //Testing
        //Reset();
    }

    public void Reset()
    {
        availableRocks = initialSpawn;
        text.text = "" + availableRocks;
        StartCoroutine(SpawnRocks());
    }

    private IEnumerator SpawnRocks()
    {
        while (spawning)
        {
            availableRocks++;
            text.text = "" + availableRocks;
            yield return new WaitForSeconds(timeBetweenRocks);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller") && availableRocks > 0)
        {
            Instantiate(rock, transform.position, transform.rotation, transform);
            availableRocks--;
            text.text = "" + availableRocks;
        }
    }

    //----------------- NO LONGER USED --------------------------
    private IEnumerator SpawnRocksDeprecated()
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
}
