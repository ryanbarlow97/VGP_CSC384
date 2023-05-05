using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class EndGameStats : MonoBehaviour
{
    private int saveSlotNumber;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI meteorsDestroyedText;
    public TextMeshProUGUI powerupsCollectedText;
    public TextMeshProUGUI playerNameText;

    public Button tryAgainButton;
    public Button backToMenuButton;

    private string playerName;
    private float survivalTime;
    private int meteorsDestroyed;
    private int powerupsCollected;

    private GameSession gameSession;
    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        saveSlotNumber = gameSession.SaveSlotNumber;
        
        playerName = gameSession.PlayerName;
        survivalTime = gameSession.SurvivalTime;
        meteorsDestroyed = gameSession.MeteorsDestroyed;
        powerupsCollected = gameSession.PowerupsCollected;

        playerNameText.text = $"{playerName}";
        survivalTimeText.text = $" {Math.Round(survivalTime/ 60.0f, 1)} minutes";
        meteorsDestroyedText.text = $" {meteorsDestroyed} meteors";
        powerupsCollectedText.text = $" {powerupsCollected} powerups";

        tryAgainButton.onClick.AddListener(TryAgain);
        backToMenuButton.onClick.AddListener(BackToMenu);
    }
     private void TryAgain()
    {
        Destroy(gameSession.gameObject);
        // End the game and reset the save slot
        SaveManager.Delete(saveSlotNumber);
        
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.playerScore = 0;
        data.playerHearts = 3;
        data.playerPosition = new SerializableVector3(0, 0, 0);
        data.playerRotation = new SerializableVector3(0, 0, 0);

        SaveManager.Save(data, saveSlotNumber);

        SceneManager.LoadScene("MainGame"); 
    }

    private void BackToMenu()
    {
        Destroy(gameSession.gameObject);
        // End the game and reset the save slot
        SaveManager.Delete(saveSlotNumber);
        SceneManager.LoadScene("MainMenu"); 
    }
}
