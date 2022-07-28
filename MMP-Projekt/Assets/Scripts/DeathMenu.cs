using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public Text wavesSurvived;
    GlobalOptions globalOptions = GlobalOptions.GetInstance();

    public void Start()
    {
        wavesSurvived.text = "" + globalOptions.survivedWaves;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Scenes/MapDesignOle");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
