using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR.OpenVR;

public class RockSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject rock;
    [SerializeField]
    float timeBetweenRocks = 2;

    float timeSinceLastRock = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastRock += Time.deltaTime;

        if (timeSinceLastRock / timeBetweenRocks > 1)
        {
            timeSinceLastRock = 0;
            Instantiate(rock, transform.position, transform.rotation, transform);
        }
    }
}
