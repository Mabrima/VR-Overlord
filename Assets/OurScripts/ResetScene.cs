using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "LeftControllerAnchor" || other.name == "RightControllerAnchor")
        {
            //This does not work. Crashes to Oculus Menu.
            SceneManager.LoadScene("World");
        }
    }
}
