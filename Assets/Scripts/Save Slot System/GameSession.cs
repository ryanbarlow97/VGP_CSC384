using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Survival time
    public float SurvivalTime { get; private set; }

    // Total play time
    public float TotalPlayTime { get; private set; }

    // Meteors destroyed
    public int MeteorsDestroyed { get; private set; }

    // Powerups collected
    public int PowerupsCollected { get; private set; }

    // Slot Number
    public int SaveSlotNumber { get; private set; }

    // Slot Number
    public string PlayerName { get; private set; }

    // Score
    public int Score { get; private set; }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        SurvivalTime += Time.deltaTime ;
    }

    public void SetTotalPlayTime(float time)
    {
        TotalPlayTime = time;
    }

    public float GetCurrentPlayTime()
    {
        return TotalPlayTime + SurvivalTime;
    }


    public void SetMeteorsDestroyed(int meteors)
    {
        MeteorsDestroyed = meteors;
    }

    public void SetPowerupsCollected(int powerups)
    {
        PowerupsCollected = powerups;
    }

    public void IncrementMeteorsDestroyed()
    {
        MeteorsDestroyed++;
    }

    public void IncrementPowerupsCollected()
    {
        PowerupsCollected++;
    }

    public void IncrementScore(int score)
    {
        Score += score;
    }

    public void SetScore(int score)
    {
        Score = score;
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void SetSaveSlotNumber(int slotNumber)
    {
        SaveSlotNumber = slotNumber;
    }
}
