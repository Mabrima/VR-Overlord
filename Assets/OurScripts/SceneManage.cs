using System.Collections;
using UnityEngine;

/* Script Author: Philip Åkerblom
 * Edits by: Johan Appelgren, Robin Arkblad
 * 
 * Not an actual scenemanager as changing scenes turn out to crash the game every time, instead chosen to merely activate different "scenes" 
 * by making empty objects holding everything that a scene should contain and turning them on and off.
 */

public class SceneManage : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;
    [SerializeField] Village village;
    [SerializeField] RockSpawner[] rockSpawners;
    [SerializeField] LightningBoltSpawner lightningboltSpawner;

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
        lightningboltSpawner.cooldown = 0;
        village.Reset();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
