using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Survival time
    public float SurvivalTime { get; private set; }

    // Meteors destroyed
    public int MeteorsDestroyed { get; private set; }

    // Powerups collected
    public int PowerupsCollected { get; private set; }

    // Slot Number
    public int SaveSlotNumber { get; private set; }

    // Slot Number
    public string PlayerName { get; private set; }

    private void Start()
    {
        PlayerName = "Player";
        SaveSlotNumber = 1;
        SurvivalTime = 0;
        MeteorsDestroyed = 0;
        PowerupsCollected = 0;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        SurvivalTime += Time.deltaTime ;
    }

    public void SetSurvivalTime(float time)
    {
        SurvivalTime = time;
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
        Debug.Log("Meteors destroyed: " + MeteorsDestroyed);
    }

    public void IncrementPowerupsCollected()
    {
        PowerupsCollected++;
        Debug.Log("Powerups collected: " + PowerupsCollected);
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
