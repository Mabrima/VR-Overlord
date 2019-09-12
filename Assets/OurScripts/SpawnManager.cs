using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Author: Kian Parsa
 * Edits by: 
 */

[System.Serializable]
public class Wave
{
    public int enemiesPerWave;
    public GameObject enemyPrefab;
}


public class SpawnManager : MonoBehaviour
{

    #region Public variables
    public Wave[] waves;
    public GameObject WinText;
    public Transform spawnPoint;
    public Transform spiderParent;
    public float timeBetweenEnemies;
    #endregion

    #region Private variables
    private int totalEnemiesInCurrentWave;
    private int enemiesInWaveLeft;
    private int spawnedEnemies;
    [SerializeField]
    private int currentWave;
    private int totalWaves;
    private int currentWavesDefeated;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentWave -= 1;
        //-1 on length because we are starting on index 0.
        totalWaves = waves.Length - 1;
        StartNextWave(); //Now the spawnmanager starts the spawning. Call this function in GameManager when ready.
        WinText.SetActive(false);
    }

    void StartNextWave()
    {
        currentWave++;

        //Win
        if (currentWave > totalWaves)
        {
            currentWavesDefeated++;

            if (currentWavesDefeated % 3 == 0) //Every third wave defeated go to shop/whatever.
            {
                if (currentWavesDefeated != 0) //dont remember if this is neccessary..
                {
                    //Do something after every third wave.
                }

            }
            return;
        }
        totalEnemiesInCurrentWave = waves[currentWave].enemiesPerWave;
        enemiesInWaveLeft = 0;
        spawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        //Spawn enemies while amount of enemies are under predetermined enemy amount.
        while (spawnedEnemies < totalEnemiesInCurrentWave)
        {

            spawnedEnemies++;
            enemiesInWaveLeft++;

            //Instantiate enemy
            GameObject enemy = waves[currentWave].enemyPrefab;
            Instantiate(enemy, spawnPoint.position, Quaternion.identity, spiderParent);
            //CreateVibration.singleton.CallVibration(0.2f, true, true); //Call this function when you want to create a vibration.

            yield return new WaitForSeconds(timeBetweenEnemies);
        }
        yield return null;

    }

    public void EnemiesDefeated() //If we have multiple waves.  --  Call this whenever an enemy dies.
    {
        enemiesInWaveLeft--;

        if (enemiesInWaveLeft == 0 && spawnedEnemies == totalEnemiesInCurrentWave)
        {

            StartNextWave();
        }

    }

    //Start waves again.
    public void NextLevel() //If we only have 1 wave.
    {
        currentWave = -1;
        StartNextWave();
    }

    public void LastWave()
    {
        if (currentWave == totalWaves)
        {
            WinText.SetActive(true);

        }
    }
}
