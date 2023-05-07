using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs;
    public string filePath;
    private TimeReplayDataList timeReplayDataList;

    private Queue<SpawnData> spawnQueue = new Queue<SpawnData>();
    private float timeElapsed;

    private void Start()
    {
        filePath = Application.dataPath + "/TimeReplayManager.json";
        // Deserialize JSON data and populate the spawnQueue
        PopulateSpawnQueue();

        // Sort the spawnQueue by the spawnedAt property
        SortSpawnQueue();
    }

    private void LateUpdate()
    {
        timeElapsed += Time.deltaTime;

        // Check if there are objects left to spawn and if it's time to spawn the next object
        while (spawnQueue.Count > 0 && timeElapsed >= spawnQueue.Peek().spawnedAt)
        {
            SpawnData spawnData = spawnQueue.Dequeue();
            SpawnObject(spawnData);
        }
    }

    private void PopulateSpawnQueue()
    {
        string json = File.ReadAllText(filePath);
        SpawnDataListWrapper spawnDataListWrapper = JsonUtility.FromJson<SpawnDataListWrapper>(json);

        // Add the SpawnData objects to the spawnQueue
        foreach (SpawnData spawnData in spawnDataListWrapper.spawnDataList)
        {
            spawnQueue.Enqueue(spawnData);
        }
    }



    private void SortSpawnQueue()
    {
        List<SpawnData> sortedList = new List<SpawnData>(spawnQueue);
        sortedList.Sort((a, b) => a.spawnedAt.CompareTo(b.spawnedAt));
        spawnQueue = new Queue<SpawnData>(sortedList);
    }

    private void SpawnObject(SpawnData spawnData)
    {
        // Instantiate the object using the spawnData properties
        GameObject objectPrefab = objectPrefabs[spawnData.prefabIndex];
        GameObject spawnedObject = Instantiate(objectPrefab, spawnData.position, spawnData.rotation);

        // Set other properties like velocity, angularVelocity, and scale
    }
}

[System.Serializable]
public class SpawnData
{
    public int prefabIndex;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public float angularVelocity;
    public Vector3 scale;
    public float spawnedAt;
    public float destroyedAt;
}

[System.Serializable]
public class SpawnDataListWrapper
{
    public List<SpawnData> spawnDataList;
}
