using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public static bool isNormalDifficulty = true;
    private GlobalOptions globalOptions = GlobalOptions.GetInstance();
    [SerializeField] public AudioSource buttonSound;

    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/MapDesignOle");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DifficultySelector()
    {
        globalOptions.SetDifficulty(!globalOptions.GetDifficulty());
    }
}
