using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenEndScreen : MonoBehaviour
{
    public GameObject EndGameMenu;
    public GameObject RestartBtn;
    public GameObject ExitBtn;
    public AudioSource backgroundMusic;

    private SnakeLikeBehaviour snakeLikeBehaviour;

    public void ShowEndGameMenu()
    {
        // Pauses the game and shows the end game screen
        Time.timeScale = 0;
        EndGameMenu.SetActive(true);

        RestartBtn.GetComponent<Button>().onClick.AddListener(Restart);
        ExitBtn.GetComponent<Button>().onClick.AddListener(Exit);

        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }

    public void Restart()
    {
        // Reloads the scene unpausing the game and resets the score
        Time.timeScale = 1;
        EndGameMenu.SetActive(false);
        ScoreUI.scoreCount = 0;

        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        snakeLikeBehaviour = GameObject.FindObjectOfType<SnakeLikeBehaviour>();
        snakeLikeBehaviour.ResetPosition();

    }

    public void Exit() 
    {
        // Closes the game or if in the editor stops the play mode
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
