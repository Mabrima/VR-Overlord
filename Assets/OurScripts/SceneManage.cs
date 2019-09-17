using System.Collections;
using UnityEngine;

/* Script Author: Philip Åkerblom
 * Edits by: Johan Appelgren, Robin Arkblad
 */

public class SceneManage : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;
    Village village;
    RockSpawner[] rockSpawners;

    private void Start()
    {
        village = FindObjectOfType<Village>();
        rockSpawners = FindObjectsOfType<RockSpawner>();
        game.SetActive(false);
        menu.SetActive(true);
    }

    public void MenuScene()
    {
        menu.SetActive(true);
        game.SetActive(false);
    }

    public void Game()
    {
        menu.SetActive(false);
        game.SetActive(true);
        foreach (RockSpawner rockSpawner in rockSpawners)
        {
            rockSpawner.Reset();
        }
        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        
        yield return new WaitForSeconds(1f);
        SpawnManager.instance.ResetWaves();
        village.ResetHealth();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
