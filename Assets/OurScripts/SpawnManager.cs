using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Kian Parsa
 * Edits by: Robin Arkblad
 */

[System.Serializable]
public class Wave
{
    public int enemiesPerWave;
    public float timeBetweenEnemies;
    public GameObject enemyPrefab;
}


public class SpawnManager : MonoBehaviour
{

    #region Public variables
    public Wave[] waves;
    public Transform spawnPoint;
    public Transform spiderParent;
    #endregion

    #region Private variables
    private int totalEnemiesInCurrentWave;
    private int enemiesInWaveLeft;
    private int spawnedEnemies;
    [SerializeField]
    private int currentWave;
    private int totalWaves;
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
        totalWaves = waves.Length - 1;
        text = FindObjectOfType<VillageTextController>();
    }

    void StartNextWave()
    {
        if (currentWave > totalWaves)
        {
            text.YouWin();
            return;
        }

        totalEnemiesInCurrentWave = waves[currentWave].enemiesPerWave;
        enemiesInWaveLeft = waves[currentWave].enemiesPerWave;
        spawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        //Spawn enemies while amount of enemies are under predetermined enemy amount.
        while (spawnedEnemies < totalEnemiesInCurrentWave)
        {
            spawnedEnemies++;

            //Instantiate enemy
            GameObject enemy = waves[currentWave].enemyPrefab;
            Instantiate(enemy, spawnPoint.position, Quaternion.identity, spiderParent);
            yield return new WaitForSeconds(waves[currentWave].timeBetweenEnemies);
        }
        currentWave++;
        yield return null;

    }

    public void EnemyDefeated() //If we have multiple waves.  --  Call this whenever an enemy dies.
    {
        enemiesInWaveLeft--;

        if (enemiesInWaveLeft == 0)
        {
            StartNextWave();
        }

    }

    public void ResetWaves()
    {
        currentWave = 0;
        StartNextWave(); //Now the spawnmanager starts the spawning. Call this function in GameManager when ready.
    }
}
