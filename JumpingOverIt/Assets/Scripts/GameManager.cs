using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gamePaused = false;
    private string saveName;
    Menu menu;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        menu = FindObjectOfType<Menu>();

        if(menu != null && SceneManager.GetActiveScene().buildIndex != 0)
        {
            this.ContinueGame();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            menu = FindObjectOfType<Menu>();

            if (menu != null)
            {
                ContinueGame();
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        GameManager.Instance.gamePaused = !GameManager.Instance.gamePaused;

        if (GameManager.Instance.gamePaused)
            PauseGame();
        else
            ContinueGame();
    }

    public void SetSaveName(string saveName)
    {
        this.saveName = saveName;
    }

    public string GetSaveName()
    {
        return this.saveName;
    }

    public void PauseGame()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        CinemachineInputProvider cameraInputController = FindObjectOfType<CinemachineInputProvider>();
        menu.PauseGame();
        playerController.enabled = false;
        cameraInputController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.gamePaused = true;
        Debug.Log("Pausando");
    }

    public void ContinueGame()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        CinemachineInputProvider cameraInputController = FindObjectOfType<CinemachineInputProvider>();
        if (playerController) playerController.enabled = true;
        if(cameraInputController) cameraInputController.enabled = true;
        menu.ResumeGame();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.gamePaused = false;
        Debug.Log("Reanudando");
    }
}
