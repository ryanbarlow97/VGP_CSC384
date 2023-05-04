using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UserDetailsInput : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button confirmButton;
    public Button cancelButton;

    public SaveSlotManager saveSlotManager;
    private int selectedSlotNumber;
    public GameObject saveSlotCanvas;
    public GameObject nameInputPanel;

    private void Awake()
    {
        confirmButton.onClick.AddListener(ConfirmName);
        cancelButton.onClick.AddListener(CancelNameInput);
        gameObject.SetActive(false);
    }

    public void ShowNameInput(SaveSlotManager manager, int slotNumber)
    {
        saveSlotManager = manager;
        selectedSlotNumber = slotNumber;
        gameObject.SetActive(true);
    }

    public void ConfirmName()
    {
        if (!string.IsNullOrEmpty(nameInputField.text))
        {
            SaveData data = new SaveData();
            data.playerName = nameInputField.text;
            data.playerLevel = 1;
            data.playerScore = 0;
            data.playerHearts = 3;
            data.playerPosition = new SerializableVector3(0, 0, 0);
            data.playerRotation = new SerializableVector3(0, 0, 0);
            SaveManager.Save(data, selectedSlotNumber);

            SceneManager.LoadScene("MainGame");
            SceneManager.sceneLoaded += (loadedScene, mode) =>
            {
                MainGameLoader mainGameLoader = FindObjectOfType<MainGameLoader>();
                if (mainGameLoader != null)
                {
                    mainGameLoader.saveSlotNumber = selectedSlotNumber;
                }
            };
        }
    }


    public void CancelNameInput()
    {
        nameInputPanel.SetActive(false);
        saveSlotCanvas.SetActive(true);
        
    }
}