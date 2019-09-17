using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR.OpenVR;

/* Script Author: Robin Arkblad
 * Edits by: Kian Parsa - Object pooling
 */

public class RockSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject rock;
    [SerializeField]
    float timeBetweenRocks = 0.5f;
    [SerializeField]
    int initialSpawn = 10;
    [SerializeField]
    TextMesh text;

    public Transform hierarchyPool;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    int availableRocks = 0;

    public bool spawning = true;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.transform.parent = hierarchyPool;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        //Testing
        Reset();
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
            GameObject rock = GetPooledObject();
            if (rock != null)
            {
                rock.transform.position = transform.position;
                rock.SetActive(true);
                rock.GetComponent<Rigidbody>().velocity = Vector3.zero;
                availableRocks--;
                text.text = "" + availableRocks;
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
