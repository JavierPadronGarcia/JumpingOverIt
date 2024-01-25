using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    public string saveName;
    public int actualLevel;
    public TextMeshProUGUI SaveName;

    private SaveService saveService;

    private void Start()
    {
        saveService = gameObject.AddComponent<SaveService>();
    }

    public void OnDeleteSaveGameClick()
    {
        saveService.DeleteSave(saveName);
    }

    public void OnLoadSavedGame()
    {
        SceneManager.LoadScene(actualLevel);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.setSaveName(this.saveName);
    }

    public void UpdateData(Save save)
    {
        this.saveName = save.saveName;
        SaveName.text = save.saveName;
        this.actualLevel = save.actualLevel;
    }
}
