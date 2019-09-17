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
        GameObject enemy = waves[currentWave].enemyPrefab;
        
        currentwaveText.text = "Current Wave: " + currentWave + 1;
        while (spawnedEnemies < totalEnemiesInCurrentWave) //Spawn enemies while amount of enemies are under predetermined enemy amount.
        {
            spawnedEnemies++;
            Instantiate(enemy, spawnPoint.position, Quaternion.identity, spiderParent);
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
}
