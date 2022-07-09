using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    // needs a relocation to a place that is better suited for global variables then this script
    public static bool isNormalDifficulty = true;
    private GlobalOptions globalOptions = GlobalOptions.GetInstance();

    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/LevelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DifficultySelector()
    {
        globalOptions.setDifficulty(!globalOptions.getDifficulty());
        Debug.Log(globalOptions.getDifficulty());
    }
}
