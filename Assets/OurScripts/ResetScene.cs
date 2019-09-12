﻿using UnityEngine;

/* Script Author: Philip Åkerblom
 * Edits by: Johan Appelgren
 */

public class ResetScene : MonoBehaviour
{
    public SceneManage sceneManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "LeftControllerAnchor" || other.name == "RightControllerAnchor")
            sceneManager.Game();
    }

}
