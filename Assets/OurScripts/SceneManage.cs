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
    }


    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Game()
    {
        SceneManager.LoadScene("World Robin");
    }

    public void Quit()
    {
        Application.Quit();
    }


}
