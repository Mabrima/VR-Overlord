using UnityEngine;
using UnityEngine.SceneManagement;

/* Script Author: 
 * Edits by: 
 */

public class SceneManage : MonoBehaviour
{

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
        SceneManager.LoadScene("World");
    }

    public void Quit()
    {
        Application.Quit();
    }


}
