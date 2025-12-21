using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;


    private void Awake() 
    {
        gameOverScreen.SetActive(false);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        // reload the same level where the player died
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // we count the first level as the main menu for the moment
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit(); // quit the game (only works on build)
        UnityEditor.EditorApplication.isPlaying = false;  // exits play mode
    }
}
