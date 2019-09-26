using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageHealth : MonoBehaviour
{
    int villageHealth = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        villageHealth = villageHealth - 1;

        if (villageHealth <= 0)
        {
            Debug.Log("Village is dead");
        }
    }




}