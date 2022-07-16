using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] public AudioSource buttonSound;
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/LevelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }
}
