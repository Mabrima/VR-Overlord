using System.Collections;
using UnityEngine;

/* Script Author: Philip Åkerblom
 * Edits by: Johan Appelgren, Robin Arkblad
 */

public class SceneManage : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;
    [SerializeField] Village village;
    [SerializeField] RockSpawner[] rockSpawners;

    private void Start()
    {
    }

    public void MenuScene()
    {
        SpawnManager.instance.TurnOffAllSpawnedObjects();
        foreach (RockSpawner rockSpawner in rockSpawners)
        {
            rockSpawner.TurnOffAllSpawnedObjects();
        }
        menu.SetActive(true);
        game.SetActive(false);
    }

    public void Game()
    {
        menu.SetActive(false);
        game.SetActive(true);
        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForFixedUpdate();

        foreach (RockSpawner rockSpawner in rockSpawners)
        {
            rockSpawner.Reset();
            rockSpawner.TurnOffAllSpawnedObjects();
        }
        SpawnManager.instance.TurnOffAllSpawnedObjects();
        SpawnManager.instance.ResetWaves();
        village.ResetHealth();
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
