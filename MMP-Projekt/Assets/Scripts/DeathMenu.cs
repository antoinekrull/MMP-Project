using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public int wavesSurvived = 2;

    public void ReplayGame()
    {
        SceneManager.LoadScene("Scenes/LevelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
