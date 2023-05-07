using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    private string achievementsFilePath;
    private AchievementsData achievementsData;

    private void Start()
    {
        achievementsFilePath = Path.Combine(Application.persistentDataPath, "achievements.json");
        LoadAchievements();
    }

    public void LoadAchievements()
    {
        if (File.Exists(achievementsFilePath))
        {
            string json = File.ReadAllText(achievementsFilePath);
            achievementsData = JsonUtility.FromJson<AchievementsData>(json);
        }
        else
        {
            achievementsData = new AchievementsData
            {
                achievements = new List<Achievement>()
                {
                    new Achievement { id = "meteor1", name = "Meteor Novice", description = "Destroy 10 meteors.", progress = 0, goal = 10, unlocked = false },
                    new Achievement { id = "meteor2", name = "Meteor Distinguished", description = "Destroy 100 meteors.", progress = 0, goal = 100, unlocked = false },
                    new Achievement { id = "meteor3", name = "Meteor Expert", description = "Destroy 1000 meteors.", progress = 0, goal = 1000, unlocked = false },
                    new Achievement { id = "meteor4", name = "Meteor Legend", description = "Destroy 10000 meteors.", progress = 0, goal = 10000, unlocked = false },
                    
                    new Achievement { id = "meteorCrusher", name = "Meteor Crusher", description = "Destroy 25 meteors without taking damage.", progress = 0, goal = 1, unlocked = false },
                    new Achievement { id = "meteorDodger", name = "Meteor Dodger", description = "Destroy 50 meteors without taking damage.", progress = 0, goal = 1, unlocked = false },
                    new Achievement { id = "meteorEvader", name = "Meteor Evader", description = "Destroy 100 meteors without taking damage.", progress = 0, goal = 1, unlocked = false },

                    new Achievement { id = "collectorSingleGame5", name = "Power Up Collector", description = "Collect 5 power-ups in a single game.", progress = 0, goal = 1, unlocked = false },
                    new Achievement { id = "collectorSingleGame10", name = "I feel invincible!", description = "Collect 10 power-ups in a single game.", progress = 0, goal = 1, unlocked = false },
                    
                    new Achievement { id = "collector1", name = "Collector 1", description = "Collect 10 power-ups.", progress = 0, goal = 10, unlocked = false },
                    new Achievement { id = "collector2", name = "Collector 2", description = "Collect 20 power-ups.", progress = 0, goal = 20, unlocked = false },
                    new Achievement { id = "collector3", name = "Collector 3", description = "Collect 30 power-ups.", progress = 0, goal = 30, unlocked = false },
                }
            };
            SaveAchievements();
        }
    }

    public void SaveAchievements()
    {
        string json = JsonUtility.ToJson(achievementsData);
        File.WriteAllText(achievementsFilePath, json);
    }
    public Achievement GetAchievement(string id)
    {
        Achievement achievement = achievementsData.achievements.Find(a => a.id == id.ToString());
        
        return achievement;
    }
    public void IncrementProgress(string id)
    {
        int amount = 1;
        Achievement achievement = achievementsData.achievements.Find(a => a.id == id);
        if (achievement != null)
        {
            achievement.progress += amount;
            if (achievement.progress >= achievement.goal)
            {
                achievement.unlocked = true;
            }
            SaveAchievements();
        }
    }
}
