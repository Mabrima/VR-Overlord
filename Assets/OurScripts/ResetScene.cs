using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    private void Start()
    {
        //all references here.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "LeftControllerAnchor" || other.name == "RightControllerAnchor")
            SceneReset();
    }

    public void SceneReset()
    {
        //call all references and reset them all to starting values.
    }
}
