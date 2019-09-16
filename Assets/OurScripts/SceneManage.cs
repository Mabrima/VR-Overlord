using System.Collections;
using UnityEngine;

/* Script Author: Philip Åkerblom
 * Edits by: Johan Appelgren
 */

public class SceneManage : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject menu;
    Village village;

    private void Start()
    {
        village = FindObjectOfType<Village>();
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
        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        
        yield return new WaitForSeconds(1f);
        SpawnManager.instance.ResetWaves();
        village.GetComponent<UnitHealth>().ResetHealth();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
