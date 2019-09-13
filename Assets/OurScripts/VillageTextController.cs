using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTextController : MonoBehaviour
{
    public GameObject LoseText;
    public GameObject WinText;

    private void Start()
    {
        ResetText();
    }

    public void GameOver()
    {
        LoseText.SetActive(true);
    }

    public void YouWin()
    {
        WinText.SetActive(true);
    }

    public void ResetText()
    {
        LoseText.SetActive(false);
        WinText.SetActive(false);
    }

}
