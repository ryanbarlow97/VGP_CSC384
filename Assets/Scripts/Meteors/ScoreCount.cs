using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreCount : MonoBehaviour
{
    GameSession gameSession;
    SaveData saveData;
    private int saveSlotNumber;
    private int score;

    private void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        saveSlotNumber = gameSession.SaveSlotNumber;
        saveData = SaveManager.Load(saveSlotNumber);
        score = saveData.playerScore;
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        UpdateLivesText();
    }
    public int GetScore()
    {
        return score;
    }
}