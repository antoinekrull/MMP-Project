using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField]
    public Text wavesSurvived;
    public string textValue;

    public void Start()
    {
        
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Scenes/MapDesignOle");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Update()
    {
        wavesSurvived.text = textValue;
    }
}
