using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTextController : MonoBehaviour
{
    public GameObject LoseText;
    public GameObject WinText;
    AudioManager music;
    bool haveLost = false;

    private void Start()
    {
        music = FindObjectOfType<AudioManager>();
        ResetText();
    }

    public void GameOver()
    {
        WinText.SetActive(false);
        LoseText.SetActive(true);
        haveLost = true;
    }

    public void YouWin()
    {
        if (!haveLost)
        {
            LoseText.SetActive(false);
            WinText.SetActive(true);
            music.Victory();
        }
    }

    public void ResetText()
    {
        LoseText.SetActive(false);
        WinText.SetActive(false);
        haveLost = false;
    }

}
