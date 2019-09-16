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
        ResetGame();
    }

    public void ResetGame()
    {
        village.GetComponent<UnitHealth>().ResetHealth();
        SpawnManager.instance.ResetWaves();

    }

    public void Quit()
    {
        Application.Quit();
    }
}
