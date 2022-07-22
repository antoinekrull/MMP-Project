using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] public AudioSource buttonSound;
    public static bool isNormalDifficulty = true;
    private GlobalOptions globalOptions = GlobalOptions.GetInstance();
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/MapDesignOle");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }

    public void DifficultySelector()
    {
        globalOptions.SetDifficulty(!globalOptions.GetDifficulty());
        Debug.Log(globalOptions.GetDifficulty());
    }
}
