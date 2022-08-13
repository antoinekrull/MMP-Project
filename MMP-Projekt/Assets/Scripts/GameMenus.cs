using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameMenus : MonoBehaviour
{
    //General
    readonly GlobalOptions globalOptions = GlobalOptions.GetInstance();
    //StartMenu
    public GameObject StartMenu;
    public static bool isNormalDifficulty = true;
    //WinMenu
    public GameObject WinMenu;
    public Text survivedTime;
    //DeathMenu
    public GameObject DeathMenu;
    public Text wavesSurvived;

    public void Start()
    {
        switch (globalOptions.gameState)
        {
            case 1: OpenWinMenu(); break;
            case 2: OpenDeathMenu(); break;
            default: OpenStartMenu(); break;
        }   
    }

    public void StartGame()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("Scenes/Beach");
    }

    void OpenWinMenu()
    {
        WinMenu.SetActive(true);
        TimeSpan t = TimeSpan.FromSeconds(globalOptions.survivedTime);
        string formattedTime = string.Format("{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
        survivedTime.text = "" + formattedTime;
    }

    void OpenDeathMenu()
    {
        DeathMenu.SetActive(true);
        wavesSurvived.text = "" + globalOptions.survivedWaves;
    }

    public void OpenStartMenu()
    {
        DeathMenu.SetActive(false); WinMenu.SetActive(false);
        StartMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DifficultySelector()
    {
        globalOptions.isNormalDifficulty = !globalOptions.isNormalDifficulty;
    }
}
