using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] SceneManage sceneManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
            sceneManager.StartGame();
    }
}
