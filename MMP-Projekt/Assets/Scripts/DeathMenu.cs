using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    int wavesSurvived;

    public void ReplayGame()
    {
        SceneManager.LoadScene("Scenes/LevelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
