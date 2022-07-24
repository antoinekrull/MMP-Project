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
        SceneManager.LoadScene("Scenes/StartMenu");
    }

    public void Update()
    {
        //wavesSurvived.text = textValue;
    }
}
