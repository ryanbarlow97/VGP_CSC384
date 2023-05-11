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
}



