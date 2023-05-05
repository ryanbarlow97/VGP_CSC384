using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class MainGameButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Hover Text:")] 
    [SerializeField] private TextMeshProUGUI originalText;
    [SerializeField] private GameObject player;
    [SerializeField] private LivesCounter livesCounter;
    private GameSession gameSession;

    private Color originalColor;
    private int saveSlotNumber;

    private void Start()
    {
        MainGameLoader mainGameLoader = FindObjectOfType<MainGameLoader>();
        if (mainGameLoader != null)
        {
            saveSlotNumber = mainGameLoader.saveSlotNumber;
        }
        originalColor = originalText.color;
        gameSession = FindObjectOfType<GameSession>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        originalText.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        originalText.color = originalColor;
    }

    public void OnBackButtonPressed()
    {
        
        SaveGameData();
        SceneManager.LoadScene("MainMenu");
        Destroy(gameSession.gameObject);
    }

    private void SaveGameData()
    {
        SaveData savedData = SaveManager.Load(saveSlotNumber);
        SaveData currentSaveData = GetCurrentSaveData(savedData);
        SaveManager.Save(currentSaveData, saveSlotNumber);
    }

    private SaveData GetCurrentSaveData(SaveData savedData)
    {
        SaveData currentSaveData = new SaveData
        {
            playerName = savedData.playerName,
            playerScore = 20,
            playerHearts = livesCounter.GetLives(),
            playerPosition = SerializableVector3.FromVector3(player.transform.position),
            playerRotation = SerializableVector3.FromVector3(player.transform.eulerAngles),
            meteorDataList = GetMeteorDataList(),
            smallMeteorDataList = GetSmallMeteorDataList(),
            powerUpDataList = GetPowerUpDataList(),

            survivalTime = gameSession.GetCurrentPlayTime(),
            meteorsDestroyed = gameSession.MeteorsDestroyed,
            
            powerupsCollected = gameSession.PowerupsCollected
        };
        Debug.Log("Survival Time: " + currentSaveData.survivalTime);
        Debug.Log("Meteors Destroyed: " + currentSaveData.meteorsDestroyed);
        Debug.Log("Powerups Collected: " + currentSaveData.powerupsCollected);
        return currentSaveData;
    }

    private List<MeteorData> GetMeteorDataList()
    {
        List<MeteorData> meteorDataList = new List<MeteorData>();
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("MeteorLarge");

        foreach (GameObject meteor in meteors)
        {
            Rigidbody2D meteorRigidbody = meteor.GetComponent<Rigidbody2D>();
            if (meteorRigidbody != null)
            {
                MeteorData meteorData = new MeteorData
                {
                    position = SerializableVector3.FromVector3(meteor.transform.position),
                    velocity = SerializableVector3.FromVector3(meteorRigidbody.velocity),
                    scale = SerializableVector3.FromVector3(meteor.transform.localScale),
                };
                meteorDataList.Add(meteorData);
            }
        }

        return meteorDataList;
    }

    private List<SmallMeteorData> GetSmallMeteorDataList()
    {
        List<SmallMeteorData> smallMeteorDataList = new List<SmallMeteorData>();
        GameObject[] smallMeteors = GameObject.FindGameObjectsWithTag("MeteorSmallBL") 
                                    .Concat(GameObject.FindGameObjectsWithTag("MeteorSmallBR"))
                                    .Concat(GameObject.FindGameObjectsWithTag("MeteorSmallTL"))
                                    .Concat(GameObject.FindGameObjectsWithTag("MeteorSmallTR"))
                                    .ToArray();

        foreach (GameObject smallMeteor in smallMeteors)
        {
            Rigidbody2D smallMeteorRigidbody = smallMeteor.GetComponent<Rigidbody2D>();
            if (smallMeteorRigidbody != null)
            {
                SmallMeteorData smallMeteorData = new SmallMeteorData
                {
                    meteorType = GetMeteorType(smallMeteor),
                    position = SerializableVector3.FromVector3(smallMeteor.transform.position),
                    velocity = SerializableVector3.FromVector3(smallMeteorRigidbody.velocity),
                    scale = SerializableVector3.FromVector3(smallMeteor.transform.localScale),
                    rotation = SerializableVector3.FromVector3(smallMeteor.transform.eulerAngles),
                    angularVelocity = new SerializableFloat(smallMeteorRigidbody.angularVelocity)
                };
                smallMeteorDataList.Add(smallMeteorData);
            }
        }

        return smallMeteorDataList;
    }

    private List<PowerUpData> GetPowerUpDataList()
    {
        List<PowerUpData> powerUpDataList = new List<PowerUpData>();
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("SpeedPowerUp") 
                                    .Concat(GameObject.FindGameObjectsWithTag("TripleFireRatePowerUp"))
                                    .ToArray();

        foreach (GameObject powerUp in powerUps)
        {
            Rigidbody2D powerUpRigidbody = powerUp.GetComponent<Rigidbody2D>();
            if (powerUpRigidbody != null)
            {
                PowerUpData powerUpData = new PowerUpData
                {
                    powerUpType = GetPowerUpType(powerUp),
                    position = SerializableVector3.FromVector3(powerUp.transform.position),
                    velocity = SerializableVector3.FromVector3(powerUpRigidbody.velocity),
                    scale = SerializableVector3.FromVector3(powerUp.transform.localScale),
                };
                powerUpDataList.Add(powerUpData);
            }
        }

        return powerUpDataList;
    }

    private int GetPowerUpType(GameObject powerUp)
    {
        int powerUpType = 0;

        if (powerUp.CompareTag("SpeedPowerUp"))
        {
            powerUpType = 0;
        }
        else if (powerUp.CompareTag("TripleFireRatePowerUp"))
        {
            powerUpType = 1;
        }
        return powerUpType;
    }

    private int GetMeteorType(GameObject smallMeteor)
    {
        int meteorType = 0;

        if (smallMeteor.CompareTag("MeteorSmallBL"))
        {
            meteorType = 0;
        }
        else if (smallMeteor.CompareTag("MeteorSmallBR"))
        {
            meteorType = 1;
        }
        else if (smallMeteor.CompareTag("MeteorSmallTL"))
        {
            meteorType = 2;
        }
        else if (smallMeteor.CompareTag("MeteorSmallTR"))
        {
            meteorType = 3;
        }

        return meteorType;
    }
}

