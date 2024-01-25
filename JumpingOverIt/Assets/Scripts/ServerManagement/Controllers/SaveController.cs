using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private SaveService saveService;

    public TMP_InputField SaveNameInputField;

    public GameObject cardPrefab;
    public Transform cardsContainer;

    private void Start()
    {
        saveService = gameObject.AddComponent<SaveService>();
    }

    public void GetSaves()
    {
        saveService.GetSaves(saves =>
        {
            cleanCards();
            foreach (var save in saves)
            {
                createCard(save);
            }
        });
    }

    private void cleanCards()
    {
        foreach (Transform child in cardsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void createCard(Save save)
    {
        GameObject card = Instantiate(cardPrefab, cardsContainer);
        CardController cardController = card.GetComponent<CardController>();
        if(cardController != null )
        {
            cardController.UpdateData(save);
        }
    }

    public void AddSave()
    {
        if (SaveNameInputField.text != "")
        {
            Save save = new();
            save.saveName = SaveNameInputField.text;
            save.actualLevel = 1;
            saveService.CreateSave(save);
        }
    }

    public void UpdateSave(string saveName, int level)
    {
        Save save = new();
        save.saveName = saveName;
        save.actualLevel = level;

        saveService.UpdateSave(save.saveName, save);
    }

    public void DeleteSave()
    {
        string saveName = SaveNameInputField.text;

        saveService.DeleteSave(saveName);
    }
}
