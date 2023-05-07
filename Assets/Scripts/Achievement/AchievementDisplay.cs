using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class AchievementDisplay : MonoBehaviour
{
    public GameObject achievementEntryPrefab;
    private string achievementsFilePath;
    Achievement achievement;

    void Start()
    {
        achievementsFilePath = Path.Combine(Application.persistentDataPath, "achievements.json");
        achievement = Achievement.Load(achievementsFilePath);

        Debug.Log(Achievement.Load(achievementsFilePath).achievements.Count);


        GameObject header = (GameObject)Instantiate(achievementEntryPrefab);
        header.transform.SetParent(this.transform);
        header.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "Achievement";
        header.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = "Description";
        header.transform.Find("Status").GetComponent<TextMeshProUGUI>().text = "Status";

        foreach (AchievementEntry achievement in achievement.achievements)
        {
            GameObject entry = (GameObject)Instantiate(achievementEntryPrefab);
            entry.transform.SetParent(this.transform);
            entry.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = achievement.name;
            entry.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = achievement.description;
            entry.transform.Find("Status").GetComponent<TextMeshProUGUI>().text = achievement.unlocked ? "Unlocked" : "Locked";
        }
    }
}
