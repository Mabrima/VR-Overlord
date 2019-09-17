using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Script Author: Kian Parsa
 * Edits by: Robin Arkblad
 */

[System.Serializable]
public class Wave
{
    public int enemiesInWave;
    public float timeBetweenEnemies;
    public GameObject enemyPrefab;
}


public class SpawnManager : MonoBehaviour
{

    #region Public variables
    public Wave[] waves;
    public Transform spawnPoint;
    public Transform spiderParent;

    public Transform hierarchyPool;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;

    public int amountToPool;

    public TextMeshPro currentwaveText;
    #endregion

    #region Private variables
    private int totalEnemiesInCurrentWave;
    private int enemiesLeftInWave;
    private int spawnedEnemies;
    [SerializeField]
    private int currentWave;
    private int endWave;
    VillageTextController text;
    #endregion

    public static SpawnManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
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

        endWave = waves.Length - 1;
        text = FindObjectOfType<VillageTextController>();
    }

    void StartNextWave()
    {
        if (currentWave >= endWave)
        {
            text.YouWin();
            return;
        }

        totalEnemiesInCurrentWave = waves[currentWave].enemiesInWave;
        enemiesLeftInWave = waves[currentWave].enemiesInWave;
        spawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        currentwaveText.text = "Current Wave: " + currentWave;
        while (spawnedEnemies < totalEnemiesInCurrentWave) //Spawn enemies while amount of enemies are under predetermined enemy amount.
        {
            spawnedEnemies++;
            if (currentWave >= endWave - 1)
            {
                GameObject enemy = waves[currentWave].enemyPrefab;
                Instantiate(enemy, spawnPoint.position, Quaternion.identity, spiderParent);
            }
            else
            {
                //Instantiate(enemy, spawnPoint.position, Quaternion.identity, spiderParent);
                GameObject enemySpider = GetPooledObject();
                if (enemySpider != null)
                {
                    enemySpider.transform.position = spawnPoint.position;
                    enemySpider.SetActive(true);
                }
            }
                yield return new WaitForSeconds(waves[currentWave].timeBetweenEnemies);
        }
        currentWave++;
        yield return null;
    }

    public void EnemyDefeated() //Call this whenever an enemy dies.
    {
        enemiesLeftInWave--;
        
        //if there are no enemies left in the wave, start the next wave.
        if (enemiesLeftInWave == 0)
        {
            StartNextWave();
        }

    }

    public void ResetWaves()
    {
        currentWave = 0;
        StartNextWave(); //Now the spawnmanager starts the spawning.
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
