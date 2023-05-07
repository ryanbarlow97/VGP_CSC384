using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections; 

public class TimeReplayManager : MonoBehaviour
{
    public List<SerializableTimeRewindData> TimeReplayDataList;

    private void Awake()
    {
        TimeReplayDataList = new List<SerializableTimeRewindData>();
    }
    
    public void SaveAllTimeRewindData()
    {
        TimeRewind[] timeRewindObjects = FindObjectsOfType<TimeRewind>();

        foreach (TimeRewind timeRewind in timeRewindObjects)
        {
            SaveObjectData(timeRewind);
        }
    }


    public void SaveObjectData(TimeRewind timeRewind)
    {
        SerializableTimeRewindData data = timeRewind.GetData();
        TimeReplayDataList.Add(data);
    }

    public void SaveToFile()
    {
        string json = JsonUtility.ToJson(new TimeReplayDataList { timeReplayDataList = TimeReplayDataList }, true);
        System.IO.File.WriteAllText(Application.dataPath + "/TimeReplayManager.json", json);
    }
}

[System.Serializable]
public class SerializableTimeRewindData
{
    public int instanceID;
    public List<PointInTime> pointsInTimeFull;
}

[System.Serializable]
public class TimeReplayDataList
{
    public List<SerializableTimeRewindData> timeReplayDataList;
}