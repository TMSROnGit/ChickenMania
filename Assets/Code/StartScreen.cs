using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameObject StartGameMenu;
    public GameObject StartBtn;
    public GameObject ExitBtn;
    public AudioSource backgroundMusic;

    void Start()
    {
        // Makes sure the game is paused when the application is started and shows the start game screen
        Time.timeScale = 0;
        StartGameMenu.SetActive(true);

        StartBtn.GetComponent<Button>().onClick.AddListener(StartGame);
        ExitBtn.GetComponent<Button>().onClick.AddListener(QuitGame);

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }

    public void StartGame()
    {
        // Unpauses the game, hides the start game screen and starts the background music if none is playing
        Time.timeScale = 1;
        StartGameMenu.SetActive(false);

        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void QuitGame()
    {
        // Closes the game or if in the editor stops the play mode
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
