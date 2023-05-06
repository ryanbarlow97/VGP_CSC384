using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public List<FrameData> gameFrames;

    private void Awake()
    {
        gameFrames = new List<FrameData>();
    }

    public void StoreFrameData(PointInTime meteorStates, PointInTime smallMeteorStates, PointInTime powerUpStates, PointInTime bulletStates, PointInTime playerShipStates)
    {
        FrameData frameData = new FrameData();
        frameData.meteorStates = meteorStates;
        frameData.smallMeteorStates = smallMeteorStates;
        frameData.powerUpStates = powerUpStates;
        frameData.bulletStates = bulletStates;
        frameData.playerShipStates = playerShipStates;

        gameFrames.Add(frameData);
    }

    private PointInTime GetState(GameObject[] gameObjects)
    {
        List<GameObjectState> gameObjectStates = new List<GameObjectState>();
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject != null)
            {
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    gameObjectStates.Add(new GameObjectState(gameObject.transform.position, gameObject.transform.rotation, rb.velocity, rb.angularVelocity, gameObject.transform.localScale, gameObject.tag));
                }
                else
                {
                    gameObjectStates.Add(new GameObjectState(gameObject.transform.position, gameObject.transform.rotation, Vector3.zero, 0f, gameObject.transform.localScale, gameObject.tag));
                }
            }
        }
        return new PointInTime(gameObjectStates);
    }

    public void SaveReplayToFile()
    {
        // Convert the gameFrames list to JSON format
        string jsonData = JsonUtility.ToJson(new SerializableFrames(gameFrames));

        // Create a file name based on the current timestamp
        string fileName = "replay_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json";

        // Create a file path in the game's data folder
        string filePath = Path.Combine(Application.dataPath, "Replays", fileName);

        // Ensure that the "Replays" folder exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Write the JSON data to the file
        File.WriteAllText(filePath, jsonData);

        // Log the saved file path
        Debug.Log("Replay saved to: " + filePath);
    }
}

[System.Serializable]
public class FrameData
{
    public PointInTime meteorStates;
    public PointInTime smallMeteorStates;
    public PointInTime powerUpStates;
    public PointInTime bulletStates;
    public PointInTime playerShipStates;
}

[System.Serializable]
public class SerializableFrames
{
    public List<FrameData> gameFrames;

    public SerializableFrames(List<FrameData> gameFrames)
    {
        this.gameFrames = gameFrames;
    }
}