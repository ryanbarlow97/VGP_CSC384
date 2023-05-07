using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Achievement
{
    public string id;
    public string name;
    public string description;
    public int progress;
    public int goal;
    public bool unlocked;
}

[System.Serializable]
public class AchievementsData
{
    public List<Achievement> achievements;
}
