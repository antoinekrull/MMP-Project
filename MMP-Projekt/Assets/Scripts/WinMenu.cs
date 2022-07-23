using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{   
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
