using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class WinMenu : MonoBehaviour
{
    public Text survivedTime;
    GlobalOptions globalOptions = GlobalOptions.GetInstance();

    public void Start()
    {
        TimeSpan t = TimeSpan.FromSeconds(globalOptions.GetSurvivedTime());
        string formattedTime = string.Format("{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
        survivedTime.text = "" + formattedTime;        
    }

    public void PlayGame()
    {       
        Debug.Log("blubberhnas");
        SceneManager.LoadScene("Scenes/MapDesignOle");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }
}
