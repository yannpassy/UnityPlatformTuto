using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header ("GameOver")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    
    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake() 
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    #region Game Over
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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // exits play mode (this code will be only executed on the editor)
        #endif
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if pause screen is active
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Pause
    public void PauseGame(bool _status)
    {
        pauseScreen.SetActive(_status);

        //Time.timeScale (0 = stop, 0.5 = slow motion, 1 = normal speed, 2 = twice faster than normal, etc)
        if(_status)
            Time.timeScale = 0; // pause the game
        else
            Time.timeScale = 1; // put the  game at the normal speed
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}
