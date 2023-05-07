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
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
    public int GetScore()
    {
        return score;
    }
}