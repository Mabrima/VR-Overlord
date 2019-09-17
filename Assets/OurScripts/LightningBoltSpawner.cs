using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltSpawner : MonoBehaviour
{
    [SerializeField] GameObject lightningBoltPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller") && lightningBoltPrefab.activeInHierarchy == false)
        {
            lightningBoltPrefab.transform.position = transform.position;
            lightningBoltPrefab.SetActive(true);
        }
    }
}
