using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;

public class ReplayManager : MonoBehaviour
{
    public string filePath;
    public GameObject bulletPrefab;
    public GameObject meteorLargePrefab;
    public GameObject playerShip;
    public GameObject[] smallMeteorPrefabs;

    private TimeReplayDataList timeReplayDataList;

    private void Start()
    {
        filePath = Application.dataPath + "/TimeReplayManager.json";
        LoadFromFile();
        Replay();
    }

    public void LoadFromFile()
    {
        string json = File.ReadAllText(filePath);
        timeReplayDataList = JsonUtility.FromJson<TimeReplayDataList>(json);
    }

    public void Replay()
    {
        StartCoroutine(ReplayCoroutine());
    }

    private IEnumerator ReplayCoroutine()
    {
        // Use a queue to store the data for efficient removal of elements
        Queue<SerializableTimeRewindData> dataQueue = new Queue<SerializableTimeRewindData>(timeReplayDataList.timeReplayDataList);

        while (dataQueue.Count > 0)
        {
            SerializableTimeRewindData data = dataQueue.Peek();
            float timeToSpawn = data.pointsInTimeFull[0].spawnedAt;

            // WaitForSeconds for the exact time until the next object should spawn
            yield return new WaitForSeconds(timeToSpawn);

            GameObject prefab = GetPrefabFromTag(data.pointsInTimeFull[0].tag);
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab);
                InitializePrefab(obj, data.pointsInTimeFull[0]);
            }
            dataQueue.Dequeue();

            // Update the remaining data's spawnedAt time by subtracting the timeToSpawn
            foreach (SerializableTimeRewindData remainingData in dataQueue)
            {
                remainingData.pointsInTimeFull[0].spawnedAt -= timeToSpawn;
            }
        }
    }


    private GameObject GetPrefabFromTag(string tag)
    {
        switch (tag)
        {
            case "Bullet":
                return bulletPrefab;
            case "MeteorLarge":
                return meteorLargePrefab;
            case "MeteorSmallBL":
                return smallMeteorPrefabs[0];
            case "MeteorSmallBR":
                return smallMeteorPrefabs[1];
            case "MeteorSmallTL":
                return smallMeteorPrefabs[2];
            case "MeteorSmallTR":
                return smallMeteorPrefabs[3];
            case "PlayerShip":
                return playerShip;
            default:
                return null;
        }
    }

    private void InitializePrefab(GameObject prefab, PointInTime pointInTime)
    {
        Debug.Log("Initializing prefab");
        prefab.transform.position = pointInTime.position;
        prefab.transform.rotation = pointInTime.rotation;
        prefab.GetComponent<Rigidbody2D>().velocity = pointInTime.velocity;
        prefab.GetComponent<Rigidbody2D>().angularVelocity = pointInTime.angularVelocity;
        prefab.transform.localScale = pointInTime.scale;
        prefab.tag = pointInTime.tag;
    }
}