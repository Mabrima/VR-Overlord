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
    }

    public void MenuScene()
    {
        menu.SetActive(true);
        game.SetActive(false);
    }

    public void Game()
    {
        ResetGame();
        menu.SetActive(false);
        game.SetActive(true);
    }

    public void ResetGame()
    {
        village.GetComponent<UnitHealth>().ResetHealth();

        //spawnmanager reset code here

    }

    public void Quit()
    {
        Application.Quit();
    }
}
