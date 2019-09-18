using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Kian Parsa
 * Edits by: 
 */

public class ObjectPooler : MonoBehaviour
{
    

    public GameObject pool;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    

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
