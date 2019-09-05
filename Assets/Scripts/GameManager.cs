using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void WonGame() //Call whenever we have won. 
    {
        //Show win-screen
        //Show score
        //Go back to menu

    }

    void LostGame() //Call whenever we have lost. 
    {
        //Show lose screen
        //Show score?
        //Go back to menu? 

    }
}
