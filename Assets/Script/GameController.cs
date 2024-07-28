using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public GameObject gameCompletePanel;
    public GameObject waveEndText;

    public GameObject gameOverMenu;


    public int currentWave = 1;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        waveEndText.SetActive(false);
        gameCompletePanel.SetActive(false);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameOVer()
    {
        
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator GameComplete()
    {

        yield return new WaitForSeconds(2f);
        waveEndText.SetActive(true);
        yield return new WaitForSeconds(3f);
        gameCompletePanel.SetActive(true);
        Time.timeScale = 0f;
        PlayerStorage.instance.waveCount++;
        PlayerStorage.instance.SaveWave();

    }

    public void StartNextMission()
    {
        currentWave++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
