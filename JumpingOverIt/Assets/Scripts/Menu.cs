using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject winText;
    public GameObject countText;
    public GameObject pauseText;
    public GameObject menuButton;
    public GameObject continueButton;
    public GameObject pausePanel;
    public GameObject createGamePanel;
    public GameObject loadGamePanel;

    private GameManager manager;
    public SaveController saveController;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void OnPlayButton ()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGameButton()
    {
        createGamePanel.SetActive(true);
    }

    public void showLoadGamePanel()
    {
        loadGamePanel.SetActive(true);
    }

    public void CancelLoadGamePanel()
    {
        loadGamePanel.SetActive(false);
    }

    public void CancelGameCreation()
    {
        createGamePanel.SetActive(false);
    }

    public void QuitButton ()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnContinueButtonClicked()
    {
        int ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log(ActiveSceneIndex + " " + manager.gamePaused);

        if (manager.gamePaused)
        {
            manager.ContinueGame();
        }
        else if(ActiveSceneIndex == 0)
        { 
            SceneManager.LoadScene(1);  
        }
        else
        {
            SceneManager.LoadScene(2);
            SaveController saveController = FindObjectOfType<SaveController>();
            saveController.UpdateSave(manager.GetSaveName(), 2);
        }
    }

    public void PauseGame()
    {
        if (pauseText) pauseText.SetActive(true);
        if (menuButton) menuButton.SetActive(true);
        if (continueButton) continueButton.SetActive(true);
        if (pausePanel) pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        if (pauseText) pauseText.SetActive(false);
        if (menuButton) menuButton.SetActive(false);
        if (continueButton) continueButton.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
    }

    public void levelFinished(int level)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (level == 1)
        {
            winText.SetActive(true);
            continueButton.SetActive(true);
            menuButton.SetActive(true);
        }

        if (level == 2)
        {
            winText.SetActive(true);
            menuButton.SetActive(true);
        }
    }

    
}
