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
    public TextMeshProUGUI ActualLevel;

    private SaveService saveService;

    private void Start()
    {
        saveService = gameObject.AddComponent<SaveService>();
    }

    public void OnDeleteSaveGameClick()
    {
        saveService.DeleteSave(saveName);
        Destroy(gameObject);
    }

    public void OnLoadSavedGame()
    {
        SceneManager.LoadScene(actualLevel);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SetSaveName(this.saveName);
    }

    public void UpdateData(Save save)
    {
        this.saveName = save.saveName;
        SaveName.text = save.saveName;
        this.actualLevel = save.actualLevel;
        ActualLevel.text = "Nivel: " + save.actualLevel.ToString();
    }
}
