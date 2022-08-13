using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class WinMenu : MonoBehaviour
{
    readonly GlobalOptions globalOptions = GlobalOptions.GetInstance();
    public Text survivedTime;

    public void Start()
    {
        TimeSpan t = TimeSpan.FromSeconds(globalOptions.survivedTime);
        string formattedTime = string.Format("{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);        
        survivedTime.text = "" + formattedTime;      
    }

    public void PlayGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("Scenes/MapDesignOle");
    }

    public void MainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("Scenes/StartMenu");
    }
}
