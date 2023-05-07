using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class AchievementEntry
{
    public string id;
    public string name;
    public string description;
    public int progress;
    public int goal;
    public bool unlocked;
}

[System.Serializable]
public class Achievement
{
    public List<AchievementEntry> achievements;

    public Achievement()
    {
        achievements = new List<AchievementEntry>();
    }

    public static Achievement Load(string filename)
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            Debug.Log("Loaded JSON: " + json);
            Achievement achievement = JsonUtility.FromJson<Achievement>(json);
            Debug.Log("Achievement count after deserialization: " + achievement.achievements.Count);
            return achievement;
        }
        Debug.Log("File does not exist: " + filename);
        return new Achievement();
    }

}



