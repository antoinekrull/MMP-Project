using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMotor : MonoBehaviour
{
    //PauseMenu
    public GameObject pauseMenu;
    GlobalOptions globalOptions = GlobalOptions.GetInstance();

    //CameraCoordinates
    public GameObject player;
    public Vector4 border = new Vector4(10, 10, -10, -10); // right - top - left - down
    private float x;
    private float y;

    void LateUpdate() // check camera bounds
    {         
        if (player)
        {            
            x = player.transform.position.x;
            y = player.transform.position.y;

            if (x > border.x) x = border.x;
            else if (x < border.z) x = border.z;

            if (y > border.y) y = border.y;
            else if (y < border.w) y = border.w;

            transform.position = new Vector3(x, y, -10);
        }
    }

    // Ingame pause menu:

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        player.GetComponent<PlayerController>().canMove = !player.GetComponent<PlayerController>().canMove;
    }

    public void QuitGame()
    {
        TogglePauseMenu();
        globalOptions.gameState = (int)GlobalOptions.gameStates.start;
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("Scenes/GameMenus");
    }
}