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
    public Transform[] spawnPoints;
    public Transform hierarchyPool;

    public List<Enemy> pooledObjects;
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
        pooledObjects = new List<Enemy>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool, spawnPoints[0].position, Quaternion.identity, hierarchyPool);
            obj.SetActive(false);
            pooledObjects.Add(obj.GetComponent<Enemy>());
        }

        endWave = waves.Length - 1;
        text = FindObjectOfType<VillageTextController>();

        //test
        //StartNextWave();

    }

    public void StartNextWave()
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

    GameObject boss;

    IEnumerator SpawnEnemies()
    {
        currentwaveText.text = "Current Wave: " + (currentWave + 1);
        int spawnLocation = 0;
        while (spawnedEnemies < totalEnemiesInCurrentWave) //Spawn enemies while amount of enemies are under predetermined enemy amount.
        {
            spawnedEnemies++;
            if (currentWave >= endWave - 1)
            {
                GameObject enemy = waves[currentWave].enemyPrefab;
                boss = Instantiate(enemy, spawnPoints[0].position, Quaternion.identity, hierarchyPool);
            }
            else
            {
                Enemy enemySpider = GetPooledObject();
                if (currentWave == 0)
                {
                    spawnLocation = 0;
                    enemySpider.SetSpawnPosition(spawnPoints[spawnLocation].position);
                }
                if (currentWave == 1)
                {
                    spawnLocation = spawnLocation == 0 ? 1 : 0;
                    enemySpider.SetSpawnPosition(spawnPoints[spawnLocation].position);
                }
                if (currentWave >= 2)
                {
                    if (spawnLocation == 2)
                        spawnLocation = 0;
                    else
                        spawnLocation++;

                    enemySpider.SetSpawnPosition(spawnPoints[spawnLocation].position);
                }
                
                SkinnedMeshRenderer rend = enemySpider.GetComponentInChildren<SkinnedMeshRenderer>();
                if (enemySpider != null)
                {
                    rend.enabled = false;
                    enemySpider.gameObject.SetActive(true);
                    yield return new WaitForFixedUpdate();
                    enemySpider.Reset();
                    rend.enabled = true;
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
        if (boss != null)
            Destroy(boss);
        currentWave = 0;
        StopAllCoroutines();
    }

    public Enemy GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void TurnOffAllSpawnedObjects()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].gameObject.SetActive(false);
        }
    }

    public void OnLose()
    {
        if (boss != null)
            Destroy(boss);
        TurnOffAllSpawnedObjects();
        StopAllCoroutines();
    }
}
