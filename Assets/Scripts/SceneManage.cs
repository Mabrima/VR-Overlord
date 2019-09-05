using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    /* Script Author: 
     * Edits by: 
     */

    // Start is called before the first frame update
    void Start()
    {
        MenuScene();
    }


    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }


}
